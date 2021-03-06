﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatePwd.aspx.cs" Inherits="WebWeChat.Net.Admin.backstage.page.kfzx.updatePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--#include file="/Admin/backstage/plugins.aspx"-->
    <link rel="stylesheet" type="text/css" href="/Admin/common/layui/css/layui.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/backstage/css/base.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/common/css/animate.css" media="all">
    <link rel="stylesheet" type="text/css" href="/Admin/backstage//css/admin.css" media="all">

    <script>
        $(function () {
            $("#form1").submit(function () {

                var params = $("#form1").serialize();
                $.ajax({
                    async: true,
                    type: "POST",
                    url: "/SysManage/UpdatePwd",
                    data: params,
                    success: function (result) {
                        if (result.rel == true) {
                            layer.msg('修改成功，请重新登录', { icon: 1, shade: 0.6, time: 2000 });
                            setTimeout("window.parent.layer.closeAll()", 1000);
                        }
                        else {
                            if (result.rel == false && result.msg == "0") {
                                layer.msg('登录超时，请重新登录', { icon: 7, shade: 0.6, time: 2000 });
                                setTimeout("window.parent.layer.closeAll()", 1000);
                            }
                            else {
                                layer.msg(result.msg, { icon: 7, shade: 0.6, time: 1000 });
                            }
                        }
                    }
                });
                return false;
            })
        })
    </script>

</head>
<body style="background-color: white">
    <form id="form1" action="#" method="post" runat="server">
        <div class="portlet-body" style="margin-top: 10px">
            <div class="form-body" id="myDiv">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label class="col-md-3 control-label">原密码:</label>

                            <div class="col-md-9">
                                <div class="input-icon">
                                    <i class="fa fa-lock fa-fw"></i>
                                    <input type="text" class="form-control" placeholder="请输入原密码" id="oldpwd" maxlength="10" name="dwmc" runat="server" required="required">
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label class="col-md-3 control-label">设置新密码:</label>
                            <div class="col-md-1">
                                <div class="input-icon">
                                    <i class="fa fa-lock fa-fw"></i>
                                    <input type="text" class="form-control" placeholder="请输入新的密码" id="newpwd" maxlength="10" name="dwlx" runat="server" required="required">
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-actions" id="saveBtn">
            <div class="col-md-12" style="text-align: center; margin-top: 10px">
                <input type="submit" value="保    存" class="layui-btn layui-btn-xm" />
            </div>
        </div>
    </form>
</body>
</html>
