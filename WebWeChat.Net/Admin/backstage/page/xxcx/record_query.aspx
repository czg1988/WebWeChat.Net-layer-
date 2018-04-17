<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="record_query.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.xxcx.record_query" %>

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
    <style>
        html {
            overflow-x: hidden
        }

        .topnav_box::-webkit-scrollbar {
            width: 5px;
            height: 10px;
            background-color: white;
        }

        .topnav_box::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
            border-radius: 10px;
            background-color: white;
        }

        .topnav_box::-webkit-scrollbar-thumb {
            border-radius: 10px;
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
            background-color: #1AA094;
        }
    </style>
</head>
<!-- END HEAD -->
<body>
    <form id="form1" runat="server">

        <div class="page-content">
            <!-- BEGIN DASHBOARD STATS 1-->

            <!--客服列表-->
            <div class="row" style="padding: 10px">
                <div class="col-lg-6 col-md-6 col-xs-12 col-sm-12">
                    <div class="portlet light bordered">
                        <div class="portlet-title tabbable-line">
                            <div class="caption">
                                <i class="icon-bubbles font-dark hide"></i>
                                <span class="caption-subject font-dark bold uppercase"><font color="red"><asp:Label runat="server" ID="nick_name"></asp:Label></font>的咨询列表</span>
                            </div>
                            <div class="actions">
                                <div class="portlet-input input-inline">
                                    <div class="input-icon right">
                                        <i class="icon-magnifier" style="cursor:pointer" id="searchIcon"></i>
                                        <input type="text" id="key"   class="form-control input-circle" placeholder="搜索..." />
                                    </div>
                                </div>
                            </div>
                            <ul class="nav nav-tabs">
                            </ul>
                        </div>
                        <div class="portlet-body">
                            <div style="max-height: 525px; overflow-y: auto" id="portlet_comments_1" class="topnav_box">
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6 col-md-6 col-xs-12 col-sm-12">
                    <!-- BEGIN PORTLET-->
                    <div class="portlet light bordered">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-bubble font-hide hide"></i>
                                <span class="caption-subject font-hide bold uppercase">会话列表（最近一次）</span>
                            </div>

                        </div>
                        <div class="portlet-body">
                            <div style="max-height: 525px; overflow-y: auto" class="topnav_box">
                                <ul class="chats" id="chats">

                                    <%--<asp:Repeater ID="dialogue_lists" runat="server">
                                        <ItemTemplate>
                                            <li <%#((DataRowView)Container.DataItem)["class"]%>>
                                                <img class="avatar" alt="" src="/Handlers/HeadImgHandler.ashx?tb=<%#((DataRowView)Container.DataItem)["tb"]%>&uin=<%#((DataRowView)Container.DataItem)["FromUin"]%>" width="80px" height="80px" />
                                                <div class="message">
                                                    <span class="arrow"></span>
                                                    <a href="javascript:;" class="name"><%#((DataRowView)Container.DataItem)["fromNickName"]%></a>
                                                    <span class="datetime"><%#((DataRowView)Container.DataItem)["systemtime"]%></span>
                                                    <span class="body"><%#((DataRowView)Container.DataItem)["DContent"]%></span>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <!-- END PORTLET-->
                </div>

            </div>

            <div class="clearfix"></div>
            <!-- END DASHBOARD STATS 1-->

        </div>
    </form>
    <script>

        $(function () {
            friend_load(<%=customer_uin%>, "");
            dialogue_load(<%=customer_uin%>, "");
            $("#searchIcon").click(function () {
                searchFriend();
            });
            $("#key").change(function () {
                searchFriend();
            });
            $("body").delegate(".friend_item", "click", function () {
                dialogue_load(<%=customer_uin%>, this.id);
            });

        })
        function searchFriend() {
            friend_load(<%=customer_uin%>, $("#key").val());
        }
        function friend_load(customer_uin, key) {
            $.ajax({
                async: true,
                type: "POST",
                data: { "customer_uin": customer_uin, "key": key },
                url: "/XxcxManage/GetFriendList",
                success: function (data) {
                    var data = data.data;
                    // console.log(data[1].Uin);
                    var friend_list_html = "";
                    if (data.length == 0) {
                        d_list_html = '<div style="text-align:center">咨询列表为空</div>';
                    }
                    else {
                        for (var i = 0; i < data.length; i++) {
                            friend_list_html += '<div  style="cursor:pointer" class="mt-comments friend_item" id="' + data[i].Uin + '">'
                                + '<div class="mt-comment">'
                                + '<div class="mt-comment-img">'
                                + '<img src="/Handlers/HeadImgHandler.ashx?tb=Csd&uin=' + data[i].Uin + '" width="50px" height="50px" />'
                                + '</div>'
                                + '<div class="mt-comment-body">'
                                + '<div class="mt-comment-info">'
                                + '<span class="mt-comment-author">' + data[i].NickName + '</span>'
                                + '</div>'
                                + '<div class="mt-comment-text" style="word-wrap:break-word;">个性签名：' + data[i].sig + '</div>'
                                + '<div class="mt-comment-details">'
                                + '<span class="mt-comment-status mt-comment-status-pending">地区：' + data[i].province + '-' + data[i].city + '</span>'
                                + '</div>'
                                + '</div>'
                                + '</div>'
                                + '</div>';
                        }
                    }
                    $("#portlet_comments_1").html(friend_list_html);
                }
            });
        }
        function dialogue_load(customer_uin,uin) {
            $.ajax({
                async: true,
                type: "POST",
                data: { "uin": uin, "customer_uin": customer_uin },
                url: "/XxcxManage/GetCsdDialogueList",
                success: function (data) {
                    var data = data.data;
                    var d_list_html = "";
                    // console.log(data[1].Uin);
                    if (data.length == 0) {
                        d_list_html = ' <li style="text-align:center">暂无聊天记录</li>';
                    }
                    else {
                        for (var i = 0; i < data.length; i++) {
                            d_list_html += ' <li ' + data[i].class + '>'
                                + '<img class="avatar" alt= "" src="/Handlers/HeadImgHandler.ashx?tb=' + data[i].tb + '&uin=' + data[i].FromUin + '" width= "80px" height= "80px" />'
                                + '<div class="message">'
                                + '<span class="arrow"></span>'
                                + '<a href="javascript:;" class="name"> ' + data[i].FromNickName + '</a>'
                                + '<span class="datetime"> ' + data[i].SysTime + '</span>'
                                + '<span class="body"> ' + data[i].DContent + '</span>'
                                + '</div>'
                                + '</li > '
                        }
                    }
                    $("#chats").html(d_list_html);
                }
            });
        }
    </script>
</body>
</html>

