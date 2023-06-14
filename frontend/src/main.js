import { createApp } from 'vue'
import App from './App.vue'
import LoginForm from "./components/Login.vue"
import vue3GoogleLogin from 'vue3-google-login'
import router from './router'
const CLIENT_ID = '671172165981-6hhoakvt37ce71htj23iilmctljro64p.apps.googleusercontent.com'
const vueApp = createApp(App)
vueApp.use(router)
vueApp.use(vue3GoogleLogin, {
    clientId: CLIENT_ID
  })
vueApp.use(CLIENT_ID)
vueApp.component('LoginForm', LoginForm)
vueApp.mount('#app')
