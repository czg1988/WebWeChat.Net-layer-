<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csd_list.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.kfzx.csd_list" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head runat="server">
    <title>XX医院诊疗辅助咨询系统——客服中心</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="客服管理、所有扫描登录过系统的客服列表，可以修改和完善客服信息" name="description" />
    <meta content="" name="author" />
    <!--#include file="/Admin/backstage/plugins.aspx"-->
    <!--layui-->
    <link rel="stylesheet" type="text/css" href="/Admin/common/layui/css/layui.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/common/css/global.css" media="all">
    <link href="/Admin/backstage/css/larryms.css" rel="stylesheet" />
    <script type="text/javascript" src="/Admin/common/layui/layui.js"></script>
    <style>
        html {
            overflow-x: hidden
        }
    </style>
</head>
<!-- END HEAD -->
<body>
    <form id="form1" runat="server">

        <div class="page-content">

        <%--    <div class="layui-side layui-bg-mycolor">
                <div class="layui-side-scroll">
                    <!-- 左侧导航区域（可配合layui已有的垂直导航） -->
                    <ul class="larryms-nav larryms-nav-tree" id="larryms_left_menu1" lay-filter="LarrySide1" data-group="0">

                        <li class="larryms-nav-item">
                            <a data-group="0" data-id="larry-1014"><i class="icon larry-icon larry-danxuankuangxuanzhong-copy"></i><cite>山大二院</cite><span class="larryms-nav-more"></span></a>
                            <dl class="larryms-nav-child">
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>内科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>呼吸内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>心血管内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>内分泌科</cite></a></dd>
                                    </dl>
                                </dd>
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>外科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>普外一科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>甲状腺外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>神经外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>周围血管病科</cite></a></dd>
                                    </dl>
                                </dd>
                            </dl>
                        </li>

                        <li class="larryms-nav-item">
                            <a data-group="0" data-id="larry-1014"><i class="icon larry-icon larry-danxuankuangxuanzhong-copy"></i><cite>齐鲁医院</cite><span class="larryms-nav-more"></span></a>
                            <dl class="larryms-nav-child">
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>内科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>呼吸内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>心血管内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>内分泌科</cite></a></dd>
                                    </dl>
                                </dd>
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>外科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>普外一科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>甲状腺外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>神经外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>周围血管病科</cite></a></dd>
                                    </dl>
                                </dd>
                            </dl>
                        </li>
                        <li class="larryms-nav-item">
                            <a data-group="0" data-id="larry-1014"><i class="icon larry-icon larry-danxuankuangxuanzhong-copy"></i><cite>山东省中医院</cite><span class="larryms-nav-more"></span></a>
                            <dl class="larryms-nav-child">
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>内科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>呼吸内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>心血管内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>内分泌科</cite></a></dd>
                                    </dl>
                                </dd>
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>外科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>普外一科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>甲状腺外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>神经外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>周围血管病科</cite></a></dd>
                                    </dl>
                                </dd>
                            </dl>
                        </li>
                        <li class="larryms-nav-item">
                            <a data-group="0" data-id="larry-1014"><i class="icon larry-icon larry-danxuankuangxuanzhong-copy"></i><cite>山东省立医院</cite><span class="larryms-nav-more"></span></a>
                            <dl class="larryms-nav-child">
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>内科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>呼吸内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>心血管内科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>内分泌科</cite></a></dd>
                                    </dl>
                                </dd>
                                <dd class="grandson"><a data-group="0" data-id="larry-1022"><i class="icon larry-icon larry-arrow_r1"></i><cite>外科</cite><span class="larryms-nav-more"></span></a>
                                    <dl class="larryms-nav-child">
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>普外一科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>甲状腺外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>神经外科</cite></a></dd>
                                        <dd class=""><a data-group="0" data-url="html/temp.html" data-id="larry-1023"><cite>周围血管病科</cite></a></dd>
                                    </dl>
                                </dd>
                            </dl>
                        </li>

                        <span class="larryms-nav-bar" style="top: 322.5px; height: 0px; opacity: 0;"></span>

                    </ul>
                </div>
            </div>--%>

            <!--客服列表-->
            <div class="row">
                <div class="col-lg-12 col-xs-12 col-sm-12">
                    <!-- BEGIN PORTLET-->
                    <div class="portlet light">
                        <div class="portlet-body">
                            <div class="row">
                                <asp:Repeater ID="csd_lists" runat="server">
                                    <ItemTemplate>
                                        <div class="col-lg-3 col-md-4 con-sm-12" style="padding: 5px">
                                            <!--begin: widget 1-1 -->
                                            <div class="mt-widget-1">
                                                <div class="mt-img">
                                                    <img  src="/Handlers/HeadImgHandler.ashx?tb=CSD&uin=<%#((DataRowView)Container.DataItem)["Uin"]%>" width="80px" height="80px"> 
                                                </div>
                                                <div class="mt-body">
                                                    <h3 class="mt-username"><a style="color:#1AA094" href="csd_info.aspx"><%#((DataRowView)Container.DataItem)["NickName"]%></a>（好友：<a style="color:#1AA094" onclick="toFriendPage(<%#((DataRowView)Container.DataItem)["Uin"]%>)" href="javascript:void(0)"><%#((DataRowView)Container.DataItem)["friends"]%></a>）</h3>
                                                    <p class="mt-user-title">妇产科（主任） </p>
                                                    <p class="mt-user-title"><a href="record_list.aspx">最后回复：预产期2018年12月8号。</a>2018-03-15 8:12</p>
                                                </div>
                                            </div>
                                            <!--end: widget 1-1 -->
                                        </div>
                                    </ItemTemplate>

                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <!-- END PORTLET-->
                </div>
            </div>
            </div>
    </form>
    <script type="text/javascript">

        layui.cache.page = 'index';
      
        layui.config({
            version: "2.0.7",
            base: '/Admin/common/'  //实际使用时，建议改成绝对路径
        }).extend({
            larry: 'js/base'
            }).use('larry');

        function toFriendPage(uin) {
            var flag = true;
            parent.layer.open({
                type: 2,
                title: '咨询列表',
                maxmin: true,
                shade: [0.5, '#393D49'],
                resize: true,
                shadeClose: false, //点击遮罩关闭层
                area: ['90%', '90%'],
                content: 'page/kfzx/friend_list.aspx?uin=' + uin,
                scrolling: 'no',
                cancel: function (layero, index) {
                    flag = false;
                    parent.layer.closeAll();
                    return false;
                },
                end: function () {
                    if (flag) {
                        //layer.msg('修改成功', { icon: 1, shade: 0.6, time: 1000 });
                        _dataTables.ajax.reload();
                    }
                }
            });
        }
    </script>

</body>
</html>
