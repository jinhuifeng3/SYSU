#include "ModifyUserScene.h"
#include "Utils.h"
#include "network/HttpClient.h"
#include "json/document.h"
#include "ui/CocosGUI.h"
#include "json/document.h"
#include "json/writer.h"
#include "json/stringbuffer.h"


using namespace cocos2d::network;
using namespace cocos2d::ui;
using namespace rapidjson;

cocos2d::Scene * ModifyUserScene::createScene() {
  return ModifyUserScene::create();
}

bool ModifyUserScene::init() {
  if (!Scene::init()) return false;
  
  auto visibleSize = Director::getInstance()->getVisibleSize();
  Vec2 origin = Director::getInstance()->getVisibleOrigin();

  auto postDeckButton = MenuItemFont::create("Post Deck", CC_CALLBACK_1(ModifyUserScene::putDeckButtonCallback, this));
  if (postDeckButton) {
    float x = origin.x + visibleSize.width / 2;
    float y = origin.y + postDeckButton->getContentSize().height / 2;
    postDeckButton->setPosition(Vec2(x, y));
  }

  auto backButton = MenuItemFont::create("Back", [](Ref* pSender) {
    Director::getInstance()->popScene();
  });
  if (backButton) {
    float x = origin.x + visibleSize.width / 2;
    float y = origin.y + visibleSize.height - backButton->getContentSize().height / 2;
    backButton->setPosition(Vec2(x, y));
  }

  auto menu = Menu::create(postDeckButton, backButton, NULL);
  menu->setPosition(Vec2::ZERO);
  this->addChild(menu, 1);

  deckInput = TextField::create("Deck json here", "arial", 24);
  if (deckInput) {
    float x = origin.x + visibleSize.width / 2;
    float y = origin.y + visibleSize.height - 100.0f;
    deckInput->setPosition(Vec2(x, y));
    this->addChild(deckInput, 1);
  }

  messageBox = Label::create("", "arial", 30);
  if (messageBox) {
    float x = origin.x + visibleSize.width / 2;
    float y = origin.y + visibleSize.height / 2;
    messageBox->setPosition(Vec2(x, y));
    this->addChild(messageBox, 1);
  }

  return true;
}

void ModifyUserScene::putDeckButtonCallback(Ref * pSender) {
  // Your code here
    HttpRequest* request = new HttpRequest();
    request->setUrl("http://127.0.0.1:8000/users");
    request->setRequestType(HttpRequest::Type::PUT);
    request->setResponseCallback(CC_CALLBACK_2(ModifyUserScene::onHttpRequestCompleted, this));
    
    rapidjson::Document doc;
    doc.SetObject();
    rapidjson::Value arr(rapidjson::kArrayType);
    rapidjson::Value obj(rapidjson::kObjectType);
    rapidjson::Document::AllocatorType& allocator = doc.GetAllocator();
    
    std::string input = deckInput->getString();
    
    rapidjson::Document temp;
    temp.Parse(input.c_str());
    
    for (int i = 0; i < temp.Size(); i++) {
        auto subArr = temp[i].GetObject();
        rapidjson::Value obj(rapidjson::kObjectType);
        for (auto j = subArr.begin(); j != subArr.end(); j++) {
            obj.AddMember(j->name, j->value, allocator);
        }
        arr.PushBack(obj, allocator);
    }
    
    doc.AddMember("deck", arr, allocator);
    
    
    StringBuffer buffer;
    rapidjson::Writer<StringBuffer> writer(buffer);
    doc.Accept(writer);
    request->setRequestData(buffer.GetString(), buffer.GetSize());
    request->setTag("Update");
    HttpClient::getInstance()->send(request);
    request->release();
}
void ModifyUserScene::onHttpRequestCompleted(HttpClient* sender,HttpResponse* response){
    if(!response){
        return;
    }
    if(!response->isSucceed()){
        CCLOG("response failed");
        return;
    }
    std::vector<char>* buffer = response->getResponseData();
    std::string json = "";
    for(int i = 0; i < buffer->size(); i++)
    {
        json += (*buffer)[i];
    }
    rapidjson::Document d;
    d.Parse<0>(json.c_str());
    messageBox->setString(d["msg"].GetString());
}
