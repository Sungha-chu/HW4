import { createWebHistory, createRouter } from "vue-router";
import HomePage from '@/components/HomePage.vue';
import RegisterAccount from '@/components/RegisterAccount.vue';
import UserInfo from '@/components/UserInfo.vue';

const routes = [
  {
    path: "/",
    name: "HomePage",
    component: HomePage,
  },
  {
    path: "/RegisterAccount",
    name: "RegisterAccount",
    component: RegisterAccount,
  },
  {
    path: "/UserInfo",
    name: "UserInfo",
    component: UserInfo,
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;