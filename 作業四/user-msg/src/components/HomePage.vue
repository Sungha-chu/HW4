<template>
    <h1>登入</h1>
    <label>電子信箱: </label><input v-model="account" type="text" placeholder="請輸入電子信箱"><br>
    <label>密碼: </label><input v-model="password" type="password" placeholder="請輸入密碼"><br>
    <router-link to="/RegisterAccount"><button>註冊</button></router-link><button v-on:click="login">送出</button>
  </template>

  <script lang="ts">
    import { defineComponent } from '@vue/runtime-core';
    import axios from 'axios';
  
    export default defineComponent({
        name: 'App',
        data(){
            return{
                account : '',
                password : '',
            }
        },
        methods: {
            
            login(){
                if(this.account != ""){
                    if(this.password != ""){
                        axios.get('https://localhost:7185/api/User/' + this.account)
                        .then(response => {
                            if(response){
                                if(response.data.password.trim() == this.password.trim()){
                                    const permission = response.data.permission.trim();
                                    this.$router.push({ path: 'UserInfo', query: { permission: `${permission}` }});
                                }
                            }else alert("87");
                        }).catch( (error) => console.log(error))
                    }else alert("密碼不能為空!");
                    
                }else alert("電子信箱不能為空!");
                
            }
        }
    });
  </script>
  
  <style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
  </style>
  