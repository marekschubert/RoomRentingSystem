import { createRouter, createWebHistory } from 'vue-router'
import FloorView from '@/components/FloorView.vue'
import Reservations from '@/components/Reservations.vue'

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
]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
