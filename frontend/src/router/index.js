import { createRouter, createWebHistory } from 'vue-router'
import FloorView from '@/components/FloorView.vue'
import Reservations from '@/components/Reservations.vue'
import LoginForm from '@/components/Login.vue'

const routes = [
  {
    path: '/',
    name: 'FloorView',
    component: FloorView
  },
  {
    path: '/reservations',
    name: 'Reservations',
    component: Reservations
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginForm    
  },
]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
