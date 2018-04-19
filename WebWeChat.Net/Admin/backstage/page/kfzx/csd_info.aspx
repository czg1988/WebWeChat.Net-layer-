<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csd_info.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.kfzx.csd_info" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>我的账户信息</title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="Author" content="larry" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">

    <!--#include file="/Admin/backstage/plugins.aspx"-->

    <link rel="Shortcut Icon" href="/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="/Admin/common/layui/css/layui.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/backstage/css/base.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/common/css/animate.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/backstage//css/admin.css" media="all">

    <script>
        $(function () {
            $(".updatePwdBtn").click(function () {
                parent.layer.open({
                    type: 2,
                    title: '修改密码',
                    maxmin: false,
                    shade: [0.5, '#393D49'],
                    resize: false,
                    shadeClose: false, //点击遮罩关闭层
                    area: ['30%', '250px'],
                    content: '/Admin/backstage/page/kfzx/updatePwd.aspx',
                    scrolling: 'no',
                    cancel: function (layero, index) {
                        flag = false;
                        layer.closeAll();
                        return false;
                    },
                    end: function () {
                        if (flag) {
                            //layer.msg('修改成功', { icon: 1, shade: 0.6, time: 1000 });
                            _dataTables.ajax.reload();
                        }
                    }
                });
            })

            $("#subBtn").click(function () {
                var realname = $("#realname").val();
                var certnumber = $("#certnumber").val();

                if ("" == realname) {
                    layer.msg('姓名不能为空', { icon: 7, shade: 0.6, time: 1000 });
                    return false;
                }
                if ("" == certnumber) {
                    layer.msg('身份证不能为空', { icon: 7, shade: 0.6, time: 1000 });
                    return false;
                }

                var params = $("#form1").serialize();
                console.log(params);
                $.ajax({
                    type: "POST",
                    url: "/Kfzx/SaveorUpdateUserInfo",
                    data: params,
                    success: function (result) {
                        if (result.rel == true) {
                            parent.layer.msg('保存成功', { icon: 1, shade: 0.6, time: 1000 });
                        }
                        else {
                            parent.layer.msg('保存失败', { icon: 7, shade: 0.6, time: 1000 });
                        }
                    }
                });
            })

        })
    </script>
</head>
<body class="larry-bg-gray" runat="server">
    <div class="layui-fluid">
        <div class="larry-container larryms-admin">
            <div class="layui-row layui-col-space20">
                <div class="layui-col-xs12 layui-col-sm4 layui-col-md3 layui-col-lg3 animated fadeInUp">
                    <section class="larry-panel">
                        <div class="admin-info">
                            <div class="photo">
                                <img src="/Handlers/HeadImgHandler.ashx?tb=CSD&uin=<%=uin%>" alt="">
                            </div>
                            <br />
                            <p class="login-time">
                                <asp:Label runat="server" ID="nick_Name"></asp:Label>
                            </p>
                            <p class="login-time">妇产科（主任）</p>
                            <p class="login-time">上次登录：<asp:Label runat="server" ID="login_Time"></asp:Label></p>
                            <p class="login-time">
                                <a class="layui-btn layui-btn-normal layui-btn-sm" onclick="toFriendPage(<%=uin%>)" href="javascript:void(0)">我的好友</a>
                                <button class="layui-btn layui-btn-danger layui-btn-sm updatePwdBtn">修改密码</button>
                            </p>

                        </div>
                        <div class="admin-log">
                            <div class="larryms-con larry-flex">
                                <div class="larry-flex-item larry-layer">
                                    <em class="larryms-stat-count">1000</em>
                                    <span>好友</span>
                                </div>
                                <div class="larry-flex-item larry-layer">
                                    <em class="larryms-stat-count">200</em>
                                    <span>公众号</span>
                                </div>
                                <div class="larry-flex-item larry-layer">
                                    <em class="larryms-stat-count">366</em>
                                    <span>加入群</span>
                                </div>
                            </div>
                        </div>
                        <div class="admin-info">
                            <div class="larryms-con larry-flex">
                                <img src="../../images/login/3.jpg" width="215" height="215" id="wxImg" runat="server">
                            </div>
                            
                        </div>
                         <div class="admin-info" style="margin-top:-20px;">
                            <div class="larryms-con larry-flex">
                                <button type="button" class="layui-btn  layui-btn-sm" id="larry_photo"><i class="layui-icon"></i>上传二维码</button>
                            </div>
                                 </div>
                    </section>
                </div>
                <div class="layui-col-xs12 layui-col-sm8 layui-col-md9 layui-col-lg9 animated fadeInRightBig">
                    <section class="larry-panel">
                        <div class="larryms-page-header">
                            <span class="tit">我的信息</span>
                        </div>
                        <div class="larryms-page-body">
                            <form class="layui-form" id="form1" method="post" enctype="multipart/form-data">
                                <div class="layui-form-item">
                                    <label class="layui-form-label">昵称</label>
                                    <div class="layui-input-block">
                                        <input type="text" id="user_uin" name="user_uin" runat="server" hidden />
                                        <input class="layui-input layui-disabled" type="text" name="nickname" id="nickname" autocomplete="off" value="" disabled="disabled" runat="server" />
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">地区</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="area" id="area" autocomplete="off" class="layui-input layui-disabled" value="" disabled="disabled" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">个性签名</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="signature" id="signature" autocomplete="off" class="layui-input layui-disabled" value="" disabled="disabled" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">登录名</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="loginname" id="loginname" autocomplete="off" class="layui-input layui-disabled" value="" disabled="disabled" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">姓名</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="realname" id="realname" autocomplete="off" class="layui-input" value="" placeholder="输入姓名" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">身份证</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="certnumber" id="certnumber" autocomplete="off" class="layui-input" placeholder="输入身份证" value="" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">手机号</label>
                                    <div class="layui-input-block">
                                        <input type="tel" name="tel" id="tel" autocomplete="off" class="layui-input" placeholder="输入手机号" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label">邮箱</label>
                                    <div class="layui-input-block">
                                        <input type="email" name="email" id="email" autocomplete="off" class="layui-input" placeholder="输入邮箱" runat="server">
                                    </div>
                                </div>
                                <div class="layui-form-item">
                                  
                                    
                                </div>

                                <div class="layui-form-item">
                                    <div class="layui-input-block">
                                        <input type="button" class="layui-btn" lay-submit="" lay-filter="demo1" value="保存" id="subBtn" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
    <!-- 加载js文件-->
    <script type="text/javascript" src="../../../common/layui/layui.js"></script>

    <script>
        layui.use('upload', function () {
            var upload = layui.upload;

            //执行实例
            var uploadInst = upload.render({
                elem: '#larry_photo',
                url: '/Upload/UploadWxImg', //上传接口
                data: { uin: '<%=uin%>' },
                accept: 'images',
                acceptMime: 'image/*',
                exts: 'jpg|png|gif|bmp|jpeg',
                size: 2048,
                multiple: false,
                drag: true,
                done: function (res, index, upload) {
                    if (res.code == 0) {
                        parent.layer.msg('上传成功', { icon: 1, shade: 0.6, time: 1000 });
                        $("#wxImg").attr("src", res.data.src);

                    } else {
                         parent.layer.msg('上传失败', { icon: 7, shade: 0.6, time: 1000 });
                    }
                }
                , error: function () {
                    //请求异常回调
                }
            });
        });
    </script>

    <script>
      

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
