const routes=[
    {path:'/home',component:home},
    {path:'/buku',component:buku},
    {path:'/mahasiswa',component:mahasiswa}
]

const router=new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')