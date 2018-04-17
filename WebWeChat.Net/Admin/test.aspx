<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebWeChat.Net.Admin.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--layui-->
    <link rel="stylesheet" type="text/css" href="common/layui/css/layui.css" media="all">
    <link rel="stylesheet" type="text/css" href="common/css/global.css" media="all">
    <link href="backstage/css/larryms.css" rel="stylesheet" />
    <script type="text/javascript" src="common/layui/layui.js"></script>
    <script src="common/plus/jquery-3.2.1.min.js"></script>
    
</head>
<body>
    <div class="layui-layout layui-layout-admin layui-fluid larryms-layout" id="larry_layout">
        <!-- 顶部导航 -->
        <div class="layui-header larryms-header" id="larry_head">
            <div class="larryms-topbar-left" id="topbarL">
                <span class="mini-logo">
                    <img src="images/logo_mini.png" alt=""></span>
                <a class="layui-logo larryms-logo">咨询管理系统</a>
                <span class="larryms-switch larryms-icon-fold" id="menufold"><i class="larry-icon larry-fold7"></i></span>
                <div class="larryms-mobile-menu" id="larrymsMobileMenu"><i class="larry-icon larry-caidan1"></i></div>
            </div>
            <div class="larryms-extend">
                <div class="larryms-topbar-menu larryms-hide-xs clearfix">
                    <ul class="larryms-nav clearfix fl" id="larryms_top_menu" lay-filter='TopMenu'>
                        <!-- 若开启顶部菜单，此处动态生成 -->

                    </ul>
                    <div class="dropdown extend-show" id="larryms_topSubMenu">
                        <i class="submenubtn larry-icon larry-sandianshu" id="subMenuButton"></i>
                        <ul class="dropdown-menu larryms-nav" id="dropdown">
                        </ul>
                    </div>
                </div>
                <!-- 右侧常用菜单 -->
                <div class="larryms-topbar-right" id="topbarR">
                    <ul class="layui-nav clearfix">
                        <li class="layui-nav-item" lay-unselect>
                            <a id="message" class="message" data-group='0' data-url='html/message.html' data-id='1004'>
                                <i class="larry-icon larry-xiaoxi2" data-icon="larry-xiaoxi2" data-font="larry-icon"></i>
                                <cite>消息</cite>
                                <span class="layui-badge-dot"></span>
                            </a>
                        </li>
                        <li class="layui-nav-item" lay-unselect>
                            <a id="lock"><i class="larry-icon larry-diannao1"></i><cite>锁屏</cite></a>
                        </li>
                        <li class="layui-nav-item" lay-unselect>
                            <a id="clearCached"><i class="larry-icon larry-qingchuhuancun1"></i><cite>清除缓存</cite></a>
                        </li>
                        <li class="layui-nav-item" lay-unselect>
                            <a id="larryTheme"><i class="larry-icon larry-zhutishezhi-"></i><cite>主题设置</cite></a>
                        </li>
                        <li class="layui-nav-item exit" lay-unselect>
                            <a id="logout" data-url='login.html'><i class="larry-icon larry-exit"></i><cite>退出</cite></a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- 内容主体 -->
        <div class="larryms-body" id="larryms_body">
            <!-- 左侧导航区域 -->
            <div class="layui-side pos-a larryms-left layui-bg-black" id="larry_left">
                <div class="layui-side-scroll">
                    <!-- 管理员信息      -->
                    <div class="user-info">
                        <div class="photo">
                            <img src="images/user1.jpg" id="user_photo" alt="">
                        </div>
                        <p><span id="uname">管理员</span>您好！欢迎登录</p>
                    </div>
                    <!-- 系统菜单 -->
                    <div class="sys-menu-box">

                        <ul class="larryms-nav larryms-nav-tree" id="larryms_left_menu1" lay-filter="LarrySide" data-group="0">
                            <li class="larryms-nav-item"><a data-group="0" data-url="main.html" data-id="larry-1001"><i class="larry-icon larry-shouye4" data-icon="larry-shouye4" data-font="larry-icon"></i><cite>后台首页</cite></a></li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1002"><i class="larry-icon larry-mianban" data-icon="larry-mianban" data-font="larry-icon"></i><cite>我的面板</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/admin.html" data-id="larry-1003"><i class="larry-icon larry-zhanghuxinxi" data-icon="larry-zhanghuxinxi" data-font="larry-icon"></i><cite>我的账户</cite></a></dd>
                                <dd><a data-group="0" data-url="html/message.html" data-id="larry-1004"><i class="larry-icon larry-xiaoxi2" data-icon="larry-xiaoxi2" data-font="larry-icon"></i><cite>消息中心</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1005"><i class="larry-icon larry-caozuorizhi" data-icon="larry-caozuorizhi" data-font="larry-icon"></i><cite>操作日志</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item "><a data-group="0" data-url="html/temp.html" data-id="larry-1006"><i class="larry-icon larry-caidanguanli3" data-icon="larry-caidanguanli3" data-font="larry-icon"></i><cite>菜单管理</cite></a></li>
                            <li class="larryms-nav-item larryms-nav-itemed"><a data-group="0" data-id="larry-1007"><i class="larry-icon larry-paikexitong_yonghuguanli" data-icon="larry-paikexitong_yonghuguanli" data-font="larry-icon"></i><cite>系统用户管理</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1008"><i class="larry-icon larry-yonghuliebiao" data-icon="larry-yonghuliebiao" data-font="larry-icon"></i><cite>用户列表</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1009"><i class="larry-icon larry-jiaoseguanli" data-icon="larry-jiaoseguanli" data-font="larry-icon"></i><cite>角色管理</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1010"><i class="larry-icon larry-quanxianguanli3" data-icon="larry-quanxianguanli3" data-font="larry-icon"></i><cite>权限管理</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1011"><i class="larry-icon larry-Shape" data-icon="larry-Shape" data-font="larry-icon"></i><cite>会员管理</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1012"><i class="larry-icon larry-Userlist1" data-icon="larry-Userlist1" data-font="larry-icon"></i><cite>会员列表</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1013"><i class="larry-icon larry-huiyuan4" data-icon="larry-huiyuan4" data-font="larry-icon"></i><cite>会员组管理</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1014"><i class="larry-icon larry-wsmp-setting" data-icon="larry-wsmp-setting" data-font="larry-icon"></i><cite>系统设置</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1015"><i class="larry-icon larry-weibiaoti2" data-icon="larry-weibiaoti2" data-font="larry-icon"></i><cite>系统基本设置</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1016"><i class="larry-icon larry-peizhiguanli" data-icon="larry-peizhiguanli" data-font="larry-icon"></i><cite>系统配置管理</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1017"><i class="larry-icon larry-shuiyin" data-icon="larry-shuiyin" data-font="larry-icon"></i><cite>图片水印设置</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1018"><i class="larry-icon larry-youjian" data-icon="larry-youjian" data-font="larry-icon"></i><cite>邮件服务器</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1019"><i class="larry-icon larry-renwu2" data-icon="larry-renwu2" data-font="larry-icon"></i><cite>计划任务管理</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1020"><i class="larry-icon larry-zifuchuanshujuji" data-icon="larry-zifuchuanshujuji" data-font="larry-icon"></i><cite>防采集串混淆</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1021"><i class="larry-icon llarry-sql" data-icon="llarry-sql" data-font="larry-icon"></i><cite>SQL命令行工具</cite></a></dd>
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="larry-icon larry-caidanguanli1" data-icon="larry-caidanguanli1" data-font="larry-icon"></i><cite>多级菜单示例</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                    <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><i class="larry-icon larry-wangzhan2" data-icon="larry-wangzhan2" data-font="larry-icon"></i><cite>第四级菜单1</cite></a></dd>
                                    <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1024"><i class="larry-icon larry-shujukuzifuchuan" data-icon="larry-shujukuzifuchuan" data-font="larry-icon"></i><cite>第四级菜单2</cite></a></dd>
                                </dl>
                                </dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1025"><i class="larry-icon larry-system-extension" data-icon="larry-system-extension" data-font="larry-icon"></i><cite>系统扩展设置</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1026"><i class="larry-icon larry-umidd17" data-icon="larry-umidd17" data-font="larry-icon"></i><cite>支付宝接口</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1027"><i class="larry-icon larry-weixin3" data-icon="larry-weixin3" data-font="larry-icon"></i><cite>微信接口</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1028"><i class="larry-icon larry-shouye-anquanguanli" data-icon="larry-shouye-anquanguanli" data-font="larry-icon"></i><cite>安全管理</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1029"><i class="larry-icon larry-anquanguanli1" data-icon="larry-anquanguanli1" data-font="larry-icon"></i><cite>基本设置</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1030"><i class="larry-icon larry-heimingdan2" data-icon="larry-heimingdan2" data-font="larry-icon"></i><cite>黑名单(IP)</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1031"><i class="larry-icon larry-sensitive" data-icon="larry-sensitive" data-font="larry-icon"></i><cite>敏感词管理</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1032"><i class="larry-icon larry-shengji2" data-icon="larry-shengji2" data-font="larry-icon"></i><cite>在线升级更新</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1033"><i class="larry-icon larry-database" data-icon="larry-database" data-font="larry-icon"></i><cite>数据库管理</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1034"><i class="larry-icon larry-shujukubeifen" data-icon="larry-shujukubeifen" data-font="larry-icon"></i><cite>数据库备份</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1035"><i class="larry-icon larry-huanyuanshujuku" data-icon="larry-huanyuanshujuku" data-font="larry-icon"></i><cite>数据库还原</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-url="html/temp.html" data-id="larry-1036"><i class="larry-icon larry-youlianguanliicon" data-icon="larry-youlianguanliicon" data-font="larry-icon"></i><cite>友链管理</cite></a></li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1037"><i class="larry-icon larry-service" data-icon="larry-service" data-font="larry-icon"></i><cite>网站维护</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1038"><i class="larry-icon larry-zhuti2" data-icon="larry-zhuti2" data-font="larry-icon"></i><cite>主题风格</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1039"><i class="larry-icon larry-qingchuhuancun" data-icon="larry-qingchuhuancun" data-font="larry-icon"></i><cite>更新系统缓存</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1040"><i class="larry-icon larry-html" data-icon="larry-html" data-font="larry-icon"></i><cite>更新首页HTML</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1041"><i class="larry-icon larry-html1" data-icon="larry-html1" data-font="larry-icon"></i><cite>更新栏目HTML</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1042"><i class="larry-icon larry-html2" data-icon="larry-html2" data-font="larry-icon"></i><cite>更新文档HTML</cite></a></dd>
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1043"><i class="larry-icon larry-xitong4" data-icon="larry-xitong4" data-font="larry-icon"></i><cite>更新网站地图</cite></a></dd>
                            </dl>
                            </li>
                            <li class="larryms-nav-item"><a data-group="0" data-id="larry-1044"><i class="larry-icon larry-shujutongji1" data-icon="larry-shujutongji1" data-font="larry-icon"></i><cite>数据统计</cite><span class="larryms-nav-more"></span></a><dl class="larryms-nav-child">
                                <dd><a data-group="0" data-url="html/temp.html" data-id="larry-1045"><i class="larry-icon larry-fcstubiao19" data-icon="larry-fcstubiao19" data-font="larry-icon"></i><cite>数据分析</cite></a></dd>
                            </dl>
                            </li>
                            <span class="larryms-nav-bar" style="top: 427.5px; height: 0px; opacity: 0;"></span></ul>
                    </div>
                </div>
            </div>


        </div>
        <!-- 底部固定区域 -->
        <div class="layui-footer larryms-footer" data-show='on' id="larry_footer">
            <div class="copyright inline-block pos-al"></div>
            <p style="text-align: center">2018 © 济南纳维 - 诊疗辅助咨询系统</p>
            <div class="larryms-info inline-block pos-ar"></div>
        </div>
    </div>
    <!-- 加载js文件-->
    <script type="text/javascript" src="../common/layui/layui.js"></script>
    <script type="text/javascript">
        // layui.cache.menusUrl = '/Admin/backstage/datas/menudatas.json';//这里设置 菜单数据项接口地址 或data参数
        // layui.cache.menusUrl = '/Handlers/MenuDataHandler.ashx';//这里设置 菜单数据项接口地址 或data参数
        // layui.cache.menusUrl = '/Test/MenuData';//这里设置 菜单数据项接口地址 或data参数
        layui.cache.page = 'index';
        //说明：并非一个页面只能加载一个模块 可以这样定义：'index,common';也并非每个页面都要定义一个模块，事实上模块根据功能需要可以公用
        //layui.cache.rightMenu = 'custom'; //默认开启页面右键菜单，设置为 custom 时需要自定义右键菜单，设置为false 关闭右键菜单
        layui.config({
            version: "2.0.7",
            base: 'common/'  //实际使用时，建议改成绝对路径
        }).extend({
            larry: 'js/base'
        }).use('larry');
    </script>
</body>
</html>
<script>
        $(function () {
            $(".larryms-nav-item").click(function () {
                this.nextAll().removeClass("larryms-nav-itemed");
                this.prevAll().removeClass("larryms-nav-itemed");
            })

        })
    </script>
