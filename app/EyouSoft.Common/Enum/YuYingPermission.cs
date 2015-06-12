﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common
{
    /// <summary>
    /// 运营后台权限 创建人:xuty 时间2010-7-30
    /// </summary>
    public enum YuYingPermission
    {
        会员管理_管理该栏目 = 25,

        注册审核_组团社审核 = 26,
        注册审核_专线商审核 = 27,
        注册审核_地接社审核 = 28,
        注册审核_景区审核 = 29,
        注册审核_酒店审核 = 30,
        注册审核_车队审核 = 31,
        注册审核_旅游用品店审核 = 32,
        注册审核_购物店审核 = 33,
        注册审核_机票供应商审核 = 108,
        注册审核_其他采购商审核 = 116,
        注册审核_随便逛逛 = 135,


        旅行社汇总管理_管理该栏目 = 119,
        旅行社汇总管理_修改 = 34,
        旅行社汇总管理_删除 = 35,

        景区汇总管理_管理该栏目 = 120,
        景区汇总管理_修改 = 36,
        景区汇总管理_删除 = 37,

        酒店汇总管理_管理该栏目 = 121,
        酒店汇总管理_修改 = 38,
        酒店汇总管理_删除 = 39,

        车队汇总管理_管理该栏目 = 122,
        车队汇总管理_修改 = 40,
        车队汇总管理_删除 = 41,

        旅游用品店汇总管理_管理该栏目 = 123,
        旅游用品店汇总管理_修改 = 42,
        旅游用品店汇总管理_删除 = 43,

        购物店汇总管理_管理该栏目 = 124,
        购物店汇总管理_修改 = 44,
        购物店汇总管理_删除 = 45,

        机票供应商管理_管理该栏目 = 125,
        机票供应商管理_修改 = 109,
        机票供应商管理_删除 = 110,

        其他采购商管理_管理该栏目 = 126,
        其他采购商管理_修改 = 117,
        其他采购商管理_删除 = 118,

        随便逛逛_管理该栏目 = 138,
        随便逛逛_修改 = 136,
        随便逛逛_删除 = 137,

        新闻中心_管理该栏目 = 46,
        新闻中心_新增 = 47,
        新闻中心_修改 = 48,
        新闻中心_删除 = 49,


        供求信息_管理该栏目 = 50,
        供求信息_修改 = 51,
        供求信息_删除 = 52,

        嘉宾访谈_管理该栏目 = 53,
        嘉宾访谈_顾问团队审核 = 54,
        嘉宾访谈_修改 = 55,
        嘉宾访谈_删除 = 56,

        同业学堂_管理该栏目 = 57,
        同业学堂_修改 = 58,
        同业学堂_删除 = 59,

        同业114广告_管理该栏目 = 60,
        同业114广告_新增 = 61,
        同业114广告_修改 = 62,
        同业114广告_删除 = 63,

        MQ广告_管理该栏目 = 64,
        MQ广告_新增 = 65,
        MQ广告_修改 = 66,
        MQ广告_删除 = 67,

        企业MQ申请审核_管理该栏目 = 68,
        企业MQ申请审核_审核 = 69,

        高级网店申请审核_管理该栏目 = 70,
        高级网店申请审核_审核 = 71,

        短信充值审核_管理该栏目 = 72,
        短信充值审核_审核 = 73,

        景区_景区管理栏目管理 = 111,
        景区_景区管理栏目新增 = 112,
        景区_景区管理栏目修改 = 113,
        景区_景区管理栏目删除 = 114,
        景区_门票管理 = 159,
        景区_门票订单 = 160,

        统计分析_管理该栏目 = 74,
        统计分析_广告投放统计 = 75,
        统计分析_广告到期统计 = 76,
        统计分析_收费MQ到期统计 = 77,
        统计分析_高级网店申请栏目 = 78,
        统计分析_高级网店申请审核 = 79,
        统计分析_高级网店到期统计 = 80,
        统计分析_短信余额统计 = 81,
        统计分析_无有效产品专线商 = 82,
        统计分析_有有效产品专线商 = 83,
        统计分析_MQ推荐专线商 = 84,
        统计分析_在线组团社 = 85,
        统计分析_组团社历史登录记录 = 86,
        统计分析_组团社行为分析 = 87,
        统计分析_专线商行为分析 = 88,

        平台管理_管理该栏目 = 89,
        平台管理_基本信息 = 90,
        平台管理_城市管理 = 91,
        平台管理_线路区域分类 = 92,
        平台管理_通用专线区域 = 93,
        平台管理_基础数据维护 = 94,
        平台管理_统计数据维护 = 95,
        平台管理_战略合作伙伴 = 96,
        平台管理_友情链接 = 97,

        高级网店反馈_管理该栏目 = 98,

        MQ反馈_管理该栏目 = 99,

        同业114平台反馈_管理该栏目 = 100,

        旅行社后台反馈_管理该栏目 = 101,

        嘉宾申请反馈_管理该栏目 = 102,

        帐户管理_管理该栏目 = 103,
        帐户管理_子帐号管理 = 104,
        帐户管理_修改密码 = 105,

        广告机票_机票查询_管理该栏目 = 107,

        给客户发送MQ消息 = 115,


        客户资料_管理该栏目 = 127,
        客户资料_新增 = 128,
        客户资料_修改 = 129,
        客户资料_删除 = 130,

        客户资料_系统维护_管理该栏目 = 131,

        酒店后台管理_订单管理 = 132,
        酒店后台管理_团队订单管理 = 133,
        酒店后台管理_首页板块数据管理 = 134,

        焦点图片管理_管理该栏目 = 139,
        新闻类别管理_管理该栏目 = 140,
        Tag管理_管理该栏目 = 141,
        关键字管理_管理该栏目 = 142,
        新闻友情链接管理_管理该栏目 = 143,
        供求信息_供求规则 = 144,
        供求信息_供求首页焦点图 = 145,
        提醒_登录下面的提醒 = 146,
        机票首页管理_特价机票管理 = 147,
        机票首页管理_团队票申请管理 = 148,
        酒店首页管理_特价酒店 = 149,
        网络营销反馈_管理栏目 = 150,

        同业中心_栏目管理 = 151,
        同业中心_新增 = 152,
        同业中心_修改 = 153,
        同业中心_删除 = 154,
        同业中心_公告广播栏目管理 = 155,
        同业中心_公告广播新增 = 156,
        同业中心_公告广播修改 = 157,
        同业中心_公告广播删除 = 158
    }
}