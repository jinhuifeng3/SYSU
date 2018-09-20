#include "HelloWorldScene.h"
#include "SimpleAudioEngine.h"
#include <string>
#include <cstring>
#pragma execution_character_set("utf-8")
#define database UserDefault::getInstance()

USING_NS_CC;
using namespace std;

bool alive = true;
char operation;

Scene* HelloWorld::createScene()
{
    return HelloWorld::create();
}

// Print useful error message instead of segfaulting when files are not there.
static void problemLoading(const char* filename)
{
    printf("Error while loading: %s\n", filename);
    printf("Depending on how you compiled you might have to add 'Resources/' in front of filenames in HelloWorldScene.cpp\n");
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
    //////////////////////////////
    // 1. super init first
    
    if ( !Scene::init() )
    {
        return false;
    }
    

    visibleSize = Director::getInstance()->getVisibleSize();
    origin = Director::getInstance()->getVisibleOrigin();
    TMXTiledMap* tmx = TMXTiledMap::create("map1.tmx");
    tmx->setPosition(visibleSize.width / 2, visibleSize.height / 2);
    tmx->setAnchorPoint(Vec2(0.5, 0.5));
    tmx->setScale(Director::getInstance()->getContentScaleFactor());
    this->addChild(tmx, 0);

	//创建一张贴图
	auto texture = Director::getInstance()->getTextureCache()->addImage("$lucia_2.png");
	//从贴图中以像素单位切割，创建关键帧
	auto frame0 = SpriteFrame::createWithTexture(texture, CC_RECT_PIXELS_TO_POINTS(Rect(0, 0, 113, 113)));
	//使用第一帧创建精灵
	player = Sprite::createWithSpriteFrame(frame0);
	player->setPosition(Vec2(origin.x + visibleSize.width / 2,
		origin.y + visibleSize.height / 2));
	addChild(player, 3);
    scoreNum = database->getIntegerForKey("score");
    
    schedule(schedule_selector(HelloWorld::AddMonster), 3.0f, kRepeatForever, 0);
    schedule(schedule_selector(HelloWorld::MonsterMove), 4.0f, kRepeatForever, 0);
    schedule(schedule_selector(HelloWorld::hitByMonster), 0.5f, kRepeatForever, 0);
    schedule(schedule_selector(HelloWorld::scoreUpdate), 0.5f, kRepeatForever, 0);
    
	//hp条
	Sprite* sp0 = Sprite::create("hp.png", CC_RECT_PIXELS_TO_POINTS(Rect(0, 320, 420, 47)));
	Sprite* sp = Sprite::create("hp.png", CC_RECT_PIXELS_TO_POINTS(Rect(610, 362, 4, 16)));
    
	//使用hp条设置progressBar
	pT = ProgressTimer::create(sp);
	pT->setScaleX(90);
	pT->setAnchorPoint(Vec2(0, 0));
	pT->setType(ProgressTimerType::BAR);
	pT->setBarChangeRate(Point(1, 0));
	pT->setMidpoint(Point(0, 1));
	pT->setPercentage(100);
	pT->setPosition(Vec2(origin.x + 14 * pT->getContentSize().width, origin.y + visibleSize.height - 2 * pT->getContentSize().height));
	addChild(pT, 1);
	sp0->setAnchorPoint(Vec2(0, 0));
	sp0->setPosition(Vec2(origin.x + pT->getContentSize().width, origin.y + visibleSize.height - sp0->getContentSize().height));
	addChild(sp0, 0);

	// 静态动画
	idle.reserve(1);
	idle.pushBack(frame0);

	// 攻击动画
	attack.reserve(17);
	for (int i = 0; i < 17; i++) {
		auto frame = SpriteFrame::createWithTexture(texture, CC_RECT_PIXELS_TO_POINTS(Rect(113 * i, 0, 113, 113)));
		attack.pushBack(frame);
	}
	// 可以仿照攻击动画
	// 死亡动画(帧数：22帧，高：90，宽：79）
	auto texture2 = Director::getInstance()->getTextureCache()->addImage("$lucia_dead.png");
    for (int i = 0; i < 22; i++) {
        auto frame = SpriteFrame::createWithTexture(texture2, CC_RECT_PIXELS_TO_POINTS(Rect(79 * i, 0, 79, 90)));
        dead.pushBack(frame);
    }

	// 运动动画(帧数：8帧，高：101，宽：68）
	auto texture3 = Director::getInstance()->getTextureCache()->addImage("$lucia_forward.png");
    for (int i = 0; i < 8; i++) {
        auto frame = SpriteFrame::createWithTexture(texture3, CC_RECT_PIXELS_TO_POINTS(Rect(68 * i, 0, 68, 101)));
        run.pushBack(frame);
    }
    
    auto Wbtn = Label::createWithSystemFont("W","arial.ttf",36);
    auto Abtn = Label::createWithSystemFont("A","arial.ttf",36);
    auto Sbtn = Label::createWithSystemFont("S","arial.ttf",36);
    auto Dbtn = Label::createWithSystemFont("D","arial.ttf",36);
    auto Xbtn = Label::createWithSystemFont("X","arial.ttf",36);
    auto Ybtn = Label::createWithSystemFont("Y","arial.ttf",36);
    time = Label::createWithSystemFont("180", "arial.ttf", 36);
    score = Label::createWithSystemFont(to_string(scoreNum), "arial.ttf", 36);
    auto WLabel = MenuItemLabel::create(Wbtn, CC_CALLBACK_1(HelloWorld::wFunc, this));
    auto ALabel = MenuItemLabel::create(Abtn, CC_CALLBACK_1(HelloWorld::aFunc, this));
    auto SLabel = MenuItemLabel::create(Sbtn, CC_CALLBACK_1(HelloWorld::sFunc, this));
    auto DLabel = MenuItemLabel::create(Dbtn, CC_CALLBACK_1(HelloWorld::dFunc, this));
    auto XLabel = MenuItemLabel::create(Xbtn, CC_CALLBACK_1(HelloWorld::xFunc, this));
    auto YLabel = MenuItemLabel::create(Ybtn, CC_CALLBACK_1(HelloWorld::yFunc, this));
    WLabel->setPosition(Vec2(50,80));
    ALabel->setPosition(Vec2(20,50));
    SLabel->setPosition(Vec2(50,50));
    DLabel->setPosition(Vec2(80,50));
    XLabel->setPosition(Vec2(700,80));
    YLabel->setPosition(Vec2(670,50));
    time->setPosition(Vec2(visibleSize.width/2,visibleSize.height -15));
    score->setPosition(Vec2(visibleSize.width/2,visibleSize.height -50));
    addChild(time);
    addChild(score);
    auto menu = Menu::create(WLabel,ALabel,SLabel,DLabel,XLabel,YLabel,NULL);
    menu->setPosition(Vec2(0,0));
    this->addChild(menu, 1);
    dtime = 180;
    
    schedule(schedule_selector(HelloWorld::subtTime), 1.0f, 180, 0);
    
    
    
    
    
    return true;
    
    
}
void HelloWorld::wFunc(Ref* pSender){
    if (!alive) {
        return;
    }
    auto run_animation = Animation::createWithSpriteFrames(run, 0.1f);
    run_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(run_animation, "runAnimation");
    if (player->getPosition().y + 20 > visibleSize.height - 20) {
        auto moveTo = MoveTo::create(0.3f,Vec2(player->getPosition().x,visibleSize.height - 20));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveTo);
    }else{
        auto moveBy = MoveBy::create(0.3f, Vec2(0,20));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveBy);
    }
}
void HelloWorld::aFunc(Ref* pSender){
    if (!alive) {
        return;
    }
    auto run_animation = Animation::createWithSpriteFrames(run, 0.1f);
    run_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(run_animation, "runAnimation");
    if (operation != 'A') {
        player->setFlippedX(true);
        operation = 'A';
    }
    if (player->getPosition().x - 20 < 15) {
        auto moveTo = MoveTo::create(0.3f,Vec2(15,player->getPosition().y));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveTo);
    }else{
        auto moveBy = MoveBy::create(0.3f, Vec2(-20,0));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation"))->reverse());
        player->runAction(moveBy);
    }
    
}
void HelloWorld::sFunc(Ref* pSender){
    if (!alive) {
        return;
    }
    auto run_animation = Animation::createWithSpriteFrames(run, 0.1f);
    run_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(run_animation, "runAnimation");
    
    if (player->getPosition().y - 20 < 25) {
        auto moveTo = MoveTo::create(0.3f,Vec2(player->getPosition().x,25));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveTo);
    }else{
        auto moveBy = MoveBy::create(0.3f, Vec2(0,-20));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveBy);
    }
    
}
void HelloWorld::dFunc(Ref* pSender){
    if (!alive) {
        return;
    }
    auto run_animation = Animation::createWithSpriteFrames(run, 0.1f);
    run_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(run_animation, "runAnimation");
    if (operation != 'D') {
        player->setFlippedX(false);
        operation = 'D';
    }
    if (player->getPosition().x + 20 > visibleSize.width - 15) {
        auto moveTo = MoveTo::create(0.3f,Vec2(visibleSize.width - 15,player->getPosition().y));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveTo);
    }else{
        auto moveBy = MoveBy::create(0.3f, Vec2(20,0));
        player->runAction(Animate::create(AnimationCache::getInstance()->getAnimation("runAnimation")));
        player->runAction(moveBy);
    }
}
void HelloWorld::xFunc(Ref* pSender){
    if (player->getActionByTag(666) != NULL || !alive) {
        return;
    }
    auto die_animation = Animation::createWithSpriteFrames(dead, 0.1f);
    die_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(die_animation, "dieAnimation");
    auto action = Animate::create(AnimationCache::getInstance()->getAnimation("dieAnimation"));
    action->setTag(666);
    player->runAction(action);
    if(pT->getPercentage() <= 0){
        pT->setPercentage(0);
        alive = false;
    }else{
        schedule(schedule_selector(HelloWorld::subHp), 0.05f, 20, 0);
    }
    
}
void HelloWorld::yFunc(Ref* pSender){
    if (player->getActionByTag(666) != NULL || !alive) {
        return;
    }
    auto attack_animation = Animation::createWithSpriteFrames(attack, 0.1f);
    attack_animation->setRestoreOriginalFrame(true);
    AnimationCache::getInstance()->addAnimation(attack_animation, "attackAnimation");
    auto action = Animate::create(AnimationCache::getInstance()->getAnimation("attackAnimation"));
    action->setTag(666);
    player->runAction(action);
    if (attackMonster()) {
        scoreNum++;
        database->setIntegerForKey("score", scoreNum);
        if(pT->getPercentage() >= 100){
            pT->setPercentage(100);
        }else{
            schedule(schedule_selector(HelloWorld::addHp), 0.05f, 20, 0);
        }
    }
}
void HelloWorld::addHp(float dt){
    pT->setPercentage(pT->getPercentage() + 1);
}
void HelloWorld::subHp(float dt){
    pT->setPercentage(pT->getPercentage() - 1);
}
void HelloWorld::subtTime(float dt){
    if(alive){
        dtime--;
        string temp = to_string(dtime);
        time->setString(temp);
        
    }else{
        time->setString("Game Over!!!");
    }
}
void HelloWorld::AddMonster(float dt){
    if (alive) {
        auto fac = Factory::getInstance();
        auto m = fac->createMonster();
        float x = random(origin.x,visibleSize.width);
        float y = random(origin.y,visibleSize.height);
        m->setPosition(x,y);
        addChild(m,3);
    }
}
void HelloWorld::MonsterMove(float dt){
    if(alive){
        auto fac = Factory::getInstance();
        fac->moveMonster(player->getPosition(), 4.0f);
        
    }
}
bool HelloWorld::attackMonster(){
    auto fac = Factory::getInstance();
    Rect playerRect = player->getBoundingBox();
    Rect attackRect = Rect(playerRect.getMinX() - 40, playerRect.getMinY(),
                           playerRect.getMaxX() - playerRect.getMinX() + 80, playerRect.getMaxY() - playerRect.getMinY());
    Sprite* collision = fac->collider(attackRect);
    if (collision != NULL) {
        removeChild(collision);
        fac->removeMonster(collision);
    }
    return collision != NULL;
}
void HelloWorld::hitByMonster(float dt){
    if (alive) {
        auto fac = Factory::getInstance();
        Sprite* collision = fac->collider(player->getBoundingBox());
        if (collision != NULL) {
            removeChild(collision);
            if(pT->getPercentage() <= 0){
                pT->setPercentage(0);
                alive = false;
                auto die_animation = Animation::createWithSpriteFrames(dead, 0.1f);
                die_animation->setRestoreOriginalFrame(true);
                AnimationCache::getInstance()->addAnimation(die_animation, "dieAnimation");
                auto action = Animate::create(AnimationCache::getInstance()->getAnimation("dieAnimation"));
                player->runAction(action);
            }else{
                schedule(schedule_selector(HelloWorld::subHp), 0.05f, 20, 0);
            }
            fac->removeMonster(collision);
        }
    }
}
void HelloWorld::scoreUpdate(float dt){
    string temp = to_string(scoreNum);
    score->setString(temp);
}
