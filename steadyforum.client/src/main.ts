import './assets/main.css'

import * as Vue from 'vue'
/*npm install vue-router*/
import * as VueRouter from 'vue-router'
/*npm install vue-cookies*/
import VueCookies from 'vue-cookies';
import App from './App.vue'

import forumpage from './components/forumpage.vue';
import chatpage from './components/chatpage.vue';
import shoppage from './components/shoppage.vue';
import thelogin from './components/thelogin.vue';

const routes = [
    {
        path: '/',
        name: 'root',
        component: thelogin,
    },
    {
        path: '/Chat',
        name: 'chat',
        component: chatpage,
    },
    {
        path: '/News',
        name: 'news',
        component: forumpage,
    },
    {
        path: '/Shop',
        name: 'shop',
        component: shoppage,
    },
]

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes,
})
const app = Vue.createApp(App)

app.use(router)
app.use(VueCookies);
app.mount('#app')

