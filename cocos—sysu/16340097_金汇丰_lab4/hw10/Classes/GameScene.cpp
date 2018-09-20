#include "GameScene.h"
#include<cstdlib>
#include<ctime>

USING_NS_CC;

Scene* GameSence::createScene()
{
	return GameSence::create();
}

// on "init" you need to initialize your instance
bool GameSence::init()
{
	//////////////////////////////
	// 1. super init first
	if (!Scene::init())
	{
		return false;
	}

	//add touch listener
	EventListenerTouchOneByOne* listener = EventListenerTouchOneByOne::create();
	listener->setSwallowTouches(true);
	listener->onTouchBegan = CC_CALLBACK_2(GameSence::onTouchBegan, this);
	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(listener, this);


	Size visibleSize = Director::getInstance()->getVisibleSize();
	Vec2 origin = Director::getInstance()->getVisibleOrigin();

    auto bg = Sprite::create("level-background-0.jpg");
    bg->setPosition(Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y ));
    this->addChild(bg, 0);
    
    stoneLayer = Layer::create();
    this->addChild(stoneLayer);
    stoneLayer->setPosition(0, 0);
    stone = Sprite::create("stone.png");
    stoneLayer->addChild(stone);
    stone->setPosition(560, 480);
    
    mouseLayer = Layer::create();
    this->addChild(mouseLayer);
    mouseLayer->setPosition(0, visibleSize.height/2);
    
    
    SpriteFrameCache::getInstance()->addSpriteFramesWithFile("level-sheet.plist");
    int totalFrames = 8;
    char frameName[20];
    Animation* mouseAnimation = Animation::create();
    
    for (int i = 0; i < totalFrames; i++)
    {
        sprintf(frameName, "gem-mouse-%d.png", i);
        mouseAnimation->addSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName(frameName));
    }
    mouseAnimation->setDelayPerUnit(0.1);
    AnimationCache::getInstance()->addAnimation(mouseAnimation, "mouseAnimation");
    
    mouse = Sprite::createWithSpriteFrameName("gem-mouse-0.png");
    Animate* mouseAnimate = Animate::create(AnimationCache::getInstance()->getAnimation("mouseAnimation"));
    mouse->runAction(RepeatForever::create(mouseAnimate));
    mouseLayer->addChild(mouse);
    mouse->setPosition(visibleSize.width/2, 0);
    auto shoot = Label::createWithSystemFont("Shoot","Marker Felt",32);
    auto labelItem = MenuItemLabel::create(shoot, CC_CALLBACK_1(GameSence::shootFunc, this));
    auto menu = Menu::create(labelItem, NULL);
    menu->setPosition(Vec2(700,500));
    this->addChild(menu, 1);
    
	return true;
}

bool GameSence::onTouchBegan(Touch *touch, Event *unused_event) {

	auto location = touch->getLocation();
    auto cheese = Sprite::create("cheese.png");
    auto loc = mouseLayer->convertToNodeSpace(location);
    
    mouseLayer->addChild(cheese);
    cheese->setPosition(loc);
    auto moveTo = MoveTo::create(2, loc);
    mouse->runAction(moveTo);
    ActionInterval *forwardOut = FadeOut::create(4.0f);
    cheese->runAction(forwardOut);
    
    
	return true;
}
void GameSence::shootFunc(Ref* pSender){
    auto originalMouseLoc = mouse->getPosition();
    //auto diamond = Sprite::create("diamond.png");
    //mouseLayer->addChild(diamond);
    //diamond->setPosition(originalMouseLoc);
    SpriteFrameCache::getInstance()->addSpriteFramesWithFile("level-sheet.plist");
    int totalFrames = 7;
    char frameName[20];
    Animation* diamondAnimation = Animation::create();
    
    for (int i = 0; i < totalFrames; i++)
    {
        sprintf(frameName, "diamond-%d.png", i);
        diamondAnimation->addSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName(frameName));
    }
    diamondAnimation->setDelayPerUnit(0.1);
    AnimationCache::getInstance()->addAnimation(diamondAnimation, "diamondAnimation");
    
    auto diamond = Sprite::createWithSpriteFrameName("diamond-0.png");
    Animate* diamondAnimate = Animate::create(AnimationCache::getInstance()->getAnimation("diamondAnimation"));
    diamond->runAction(RepeatForever::create(diamondAnimate));
    mouseLayer->addChild(diamond);
    diamond->setPosition(originalMouseLoc);
    
    
    
    
    
    
    auto shootStone = Sprite::create("stone.png");
    stoneLayer->addChild(shootStone);
    shootStone->setPosition(560,480);
    auto worldLoc = mouseLayer->convertToWorldSpace(mouse->getPosition());
    auto newLoc = stoneLayer->convertToNodeSpace(worldLoc);
    auto vec = Vec2(10*(newLoc.x - 560), 10*(newLoc.y - 480));
    auto moveBy = MoveBy::create(6, vec);
    auto moveTo = MoveTo::create(1, newLoc);
    shootStone->runAction(moveTo);
    ActionInterval *forwardOut = FadeOut::create(8.0f);
    shootStone->runAction(forwardOut);
    auto seq = Sequence::create(moveTo,forwardOut, NULL);
    auto visibleSize = Director::getInstance()->getVisibleSize();
    float randomX = rand()/double(RAND_MAX);
    float randomY = rand()/double(RAND_MAX);
    float loc_x = randomX * visibleSize.width;
    float loc_y = randomY * visibleSize.height;
    auto mouseMove = mouseLayer->convertToNodeSpace(Vec2(loc_x,loc_y));
    auto MouseMoveTo = MoveTo::create(2, mouseMove);
    mouse->runAction(MouseMoveTo);
    
}
