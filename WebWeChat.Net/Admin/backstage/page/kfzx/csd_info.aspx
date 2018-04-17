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
                 layer.open({
                    type: 2,
                    title: '修改密码',
                    maxmin: false,
                    shade: [0.5, '#393D49'],
                    resize: false,
                    shadeClose: false, //点击遮罩关闭层
                    area: ['30%', '250px'],
                    content:'updatePwd.aspx',
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

        })
    </script>
</head>
<body class="larry-bg-gray">
<div class="layui-fluid">
    <div class="larry-container larryms-admin">
          <div class="layui-row layui-col-space20">
          	    <div class="layui-col-xs12 layui-col-sm4 layui-col-md3 layui-col-lg3 animated fadeInUp">
                     <section class="larry-panel">
                     	 <div class="admin-info">
                     	 	<div class="photo">
                                <img src="/Admin/backstage/images/user1.jpg" alt="">
                            </div>
                             <br/>
                            <p class="login-time">陈金才</p>
                            <p class="login-time">省立医院妇产科（主任）</p>
                            <p class="login-time">上次登录：2017-12-18 10:35:31</p>
                            <p class="login-time"><button class="layui-btn layui-btn-normal layui-btn-sm">我的好友</button> <button class="layui-btn layui-btn-danger layui-btn-sm updatePwdBtn">修改密码</button></p>
 
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
                     	 		<img src="../../images/login/3.jpg" / width="100%" height="100%">
                     	 	</div>
                     	 </div>
                     </section>
          	    </div>
          	    <div class="layui-col-xs12 layui-col-sm8 layui-col-md9 layui-col-lg9 animated fadeInRightBig">
                    <section class="larry-panel">
                   	    <div class="larryms-page-header">
                   	    	<span class="tit">我的账户信息</span>
                   	    </div>
                   	    <div class="larryms-page-body">
                   	    	  <form class="layui-form" action="" method="post" enctype="multipart/form-data">
                   	    	  	    <div class="layui-form-item">
                   	    	  	   	     <label class="layui-form-label">用户名</label>
                   	    	  	   	     <div class="layui-input-block">
                   	    	  	   	     	   <input class="layui-input layui-disabled" type="text" name="title" autocomplete="off" value="larry" disabled="disabled" />
                   	    	  	   	     </div>
                   	    	  	    </div>
                   	    	  	    <div class="layui-form-item">
				        	                     <label class="layui-form-label">所属角色</label>
				        	                     <div class="layui-input-block">
				        	           	              <input type="text" name="username"  autocomplete="off" class="layui-input layui-disabled" value="超级管理员" disabled="disabled">
				        	                     </div>
				                          </div>
				                          <div class="layui-form-item">
				                          	   <label class="layui-form-label">真实姓名</label>
				                          	   <div class="layui-input-block">
				                          	   	    <input type="text" name="username"  autocomplete="off" class="layui-input" value="Larry">
				                          	   </div>
				                          </div>
				                          <div class="layui-form-item">
				                          	   <label class="layui-form-label">手机号码</label>
				                          	   <div class="layui-input-block">
				                          	   	    <input type="text" name="username"  autocomplete="off" class="layui-input" placeholder="输入手机号码" value="18888888888">
				                          	   </div>
				                          </div>
                                  <div class="layui-form-item">
                                       <label class="layui-form-label">密码</label>
                                       <div class="layui-input-block">
                                            <input type="text" name="password" autocomplete="off" class="layui-input larry-password" placeholder="留空则使用当前密码不进行更改"> 
                                       </div>
                                  </div>
                                  <div class="layui-form-item">
                                       <label class="layui-form-label">手机验证码</label>
                                       <div class="layui-input-block">
                                            <input type="text" name="code" autocomplete="off" class="layui-input larry-code" placeholder="请输入6位数字验证码">
                                            <div class="layui-btn" id="getCode">获取验证码</div>
                                       </div>
                                  </div>
				                          <div class="layui-form-item">
				                   	           <label class="layui-form-label">性别</label>
				                   	           <div class="layui-input-block">
				                   	       	        <input type="radio" name="sex" value="男" title="男" checked=""><div class="layui-unselect layui-form-radio layui-form-radioed"><i class="layui-anim layui-icon"></i><span>男</span></div>
				                   	       	        <input type="radio" name="sex" value="女" title="女">
                                                <div class="layui-unselect layui-form-radio">
                                                    <i class="layui-anim layui-icon"></i>
                                                    <span>女</span>
                                                </div>
				                   	           </div>
				                          </div>
                                  <div class="layui-form-item">
                                       <label class="layui-form-label">修改头像</label>
                                       <div class="layui-input-block">
                                            <button type="button" class="layui-btn" id="larry_photo"><i class="layui-icon"></i>上传文件</button>
                                       </div>
                                  </div>
                                  <div class="layui-form-item layui-form-text">
                                       <label class="layui-form-label">座右铭</label>
                                       <div class="layui-input-block">
                                            <textarea placeholder="既然选择了远方，便只顾风雨兼程；路漫漫其修远兮，吾将上下而求索" value="" class="layui-textarea"></textarea>
                                       </div>
                                  </div>
        
                                  <div class="layui-form-item">
                                      <div class="layui-input-block">
                                            <button class="layui-btn" lay-submit="" lay-filter="demo1">立即提交</button>
                                            <button type="reset" class="layui-btn layui-btn-primary">重置</button>
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
layui.cache.page = 'mypanel';
layui.config({
   version:"2.0.7",
   base:'../../../common/'  //这里实际使用时，建议改成绝对路径
}).extend({
    larry:'js/base'
}).use('larry');
</script>
</body>
</html>
