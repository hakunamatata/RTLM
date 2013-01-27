/**
 * config
 */

var path = require('path');

exports.config = {
    debug: true,
    name: 'RTLM',
    description: 'RTLM 是一种有限区域间快速反应的物流体系',
    version: '0.0.1',

    // site settings
    site_headers: [
        '<meta name="author" content="Leo" />',
    ],
    host: 'localhost',
    port: 3000,
    site_logo: '', // default is `name`
    site_navs: [
        // [ path, title, [target=''] ]
        //[ '/about', '关于' ],
    ],
    site_static_host: '', // 静态文件存储域名
    site_enable_search_preview: false, // 开启google search preview
    site_google_search_domain:  '',//'cnodejs.org',  // google search preview中要搜索的域名

    upload_dir: path.join(__dirname, 'public', 'user_data', 'images'),

    db: 'mongodb://127.0.0.1/test',
    session_secret: 'node_rtlm',

    // admin 可删除话题，编辑标签，设某人为达人
    admins: { admin: true },
    auth_cookie_name: 'node_rtlm'


    // 话题列表显示的话题数量
    //list_topic_count: 20,


    // mail SMTP
//    mail_port: 25,
//    mail_user: 'club',
//    mail_pass: 'club',
//    mail_host: 'smtp.126.com',
//    mail_sender: 'club@126.com',
//    mail_use_authentication: true,
//
//    //weibo app key
//    weibo_key: 10000000,
//    // [ { name: 'plugin_name', options: { ... }, ... ]
//    plugins: [
//        // { name: 'onehost', options: { host: 'localhost.cnodejs.org' } },
//        // { name: 'wordpress_redirect', options: {} }
//    ]
};
