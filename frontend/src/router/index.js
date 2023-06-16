import { createRouter, createWebHistory } from 'vue-router'
import FloorView from '@/components/FloorView.vue'
import LoginForm from '@/components/Login.vue'
import MyReservations from '@/components/MyReservations.vue'

const routes = [
  {
    path: '/',
    name: 'FloorView',
    component: FloorView    
  },
  {
    path: '/login',
    name: "LoginForm",
    component: LoginForm
  },
  {
    path: '/reservations',
    name: 'MyReservations',
    component: MyReservations
  },
]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
