<template>
    <h1>註冊</h1>
    <label>電子信箱: </label><input v-model="account" type="text" placeholder="請輸入電子信箱"><br>
    <label>密碼: </label><input v-model="password" type="password" placeholder="請輸入密碼"><br>
    <label>確認密碼: </label><input v-model="check_password" type="password" placeholder="請重新輸入密碼"><br>
    <label>權限: </label>
    <select v-model='permission' class='form-control'>                                                                
        <option value='create'>新增</option>
        <option value='edit'>修改</option>
        <option value='delete'>刪除</option>
    </select><br>
    <button v-on:click="send">送出</button>
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
                check_password : '',
                permission : ''
            }
        },
        methods: {
            send(){
                if(this.account != ""){
                    if(this.password == this.check_password){
                        //post
                        let data = {
                            "account": this.account,
                            "password": this.password,
                            "permission": this.permission
                        }
                        axios.post('https://localhost:7185/api/User',data)
                        .then(response => {
                            alert("註冊成功!");
                            this.$router.push("/");
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                    }else alert("密碼與確認密碼不同，請重新確認!");
                }else alert("電子信箱不能為空!");
            },

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
  