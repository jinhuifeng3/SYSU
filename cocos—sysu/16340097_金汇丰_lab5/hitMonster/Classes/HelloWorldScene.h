#pragma once
#include "cocos2d.h"
#include "Monster.h"
using namespace cocos2d;

class HelloWorld : public cocos2d::Scene
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    void wFunc(Ref* pSender);
    void aFunc(Ref* pSender);
    void sFunc(Ref* pSender);
    void dFunc(Ref* pSender);
    void xFunc(Ref* pSender);
    void yFunc(Ref* pSender);
    void addHp(float dt);
    void subHp(float dt);
    void subtTime(float dt);
    void AddMonster(float dt);
    void MonsterMove(float dt);
    void scoreUpdate(float dt);
    bool attackMonster();
    void hitByMonster(float dt);
    // implement the "static create()" method manually
    CREATE_FUNC(HelloWorld);
private:
	cocos2d::Sprite* player;
	cocos2d::Vector<SpriteFrame*> attack;
	cocos2d::Vector<SpriteFrame*> dead;
	cocos2d::Vector<SpriteFrame*> run;
	cocos2d::Vector<SpriteFrame*> idle;
	cocos2d::Size visibleSize;
	cocos2d::Vec2 origin;
	cocos2d::Label* time;
    cocos2d::Label* score;
	int dtime;
    int scoreNum = 0;
	cocos2d::ProgressTimer* pT;
};
