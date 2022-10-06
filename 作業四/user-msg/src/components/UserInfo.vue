<template>
    <h1>用戶資訊</h1>
    <div>您的權限 : {{getPermission()}}</div>
    <div class="tableDiv">
        <table class="table">
            <thead>
                <td>工單編號</td>
                <td>工單訊息</td>
            </thead>
            <tbody>
                <tr v-for="item in result" :key="item.logMsg">
                    <td>{{ item.id }}</td>
                    <td>{{ item.logMsg }}</td>
                </tr>
            </tbody>
        </table>
        <button v-on:click="click()">{{getPermission()}}</button>
        <br/>

        <table class="table2">
            <thead>
                <td>工單編號</td>
                <td>工單上傳或更新時間</td>
                <td>工單設計圖</td>
            </thead>
            <tbody>
                <tr v-for="item in result2" :key="item.path">
                    <td>{{ item.id }}</td>
                    <td>{{ item.time }}</td>
                    <td><img v-bind:src="'https://localhost:7185' + item.path"></td>
                </tr>
            </tbody>
        </table>
        <button v-on:click="clickImg()">{{getPermission()}}設計圖</button>

        <div v-if="isPopup" class="vue-popUpSmall" :style="popPosition">
            <slot/>
            <slot name="footer">
                <p style="font-size:25px;">{{getPermission()}}</p>
                <div v-if="inputId" >工單編號 : <input type="text" placeholder="請輸入工單編號" v-model="id" /></div>
                <div v-if="inputMsg" >工單訊息 : <input type="text" placeholder="請輸入工單訊息" v-model="msg" /></div>
                <div class="text-center">
                <div class="button">
                    <button v-on:click="send();">送出</button>
                </div>
                <div class="button">
                    <button v-on:click="cancel">取消</button>
                </div>
                </div>
            </slot>
        </div>

        <div v-if="isPopup2" class="vue-popUpSmall" :style="popPosition">
            <slot/>
            <slot name="footer">
                <p style="font-size:25px;">{{getPermission()}}</p>
                <div v-if="inputId" >工單編號 : <input type="text" placeholder="請輸入工單編號" v-model="id" /></div>
                <div v-if="inputMsg" >工單設計圖 : <input type="file" placeholder="請上傳供單設計圖" @change="changeFile($event)"/></div>
                <div class="text-center">
                <div class="button">
                    <button v-on:click="sendImg();">送出</button>
                </div>
                <div class="button">
                    <button v-on:click="cancel">取消</button>
                </div>
                </div>
            </slot>
        </div>
    </div>
  </template>

  <script lang="ts">
    import { defineComponent } from '@vue/runtime-core';
    import axios from 'axios';
  
    export default defineComponent({
        name: 'App',
        props: {
            value: {
                type: [Number, String]
            },
             position: {
                type: String,
                default: '0'
            }
        },
        data(){
            return{
                result:[
                    {
                        id:"",
                        logMsg:"",
                    }
                ],
                result2:[
                    {
                        id:"",
                        time:"",
                        path:"",
                    }
                ],
                permission :"",
                inputId:false,
                inputMsg:false,
                isPopup: false,
                isPopup2: false,
                id:"",
                msg:"",
                idImg:"",
                imgFile:""
            }
        },
        computed: {
            popPosition() {
                return `margin-left:${this.position}`
            }
        },
        mounted() {
            this.getMsg();
            this.getImg();
        },
        methods: {
            getPermission() {
                switch(this.$route.query.permission){
                    case "create":
                        this.inputId = false;
                        this.inputMsg = true;
                        return  "新增工單";
                    case "edit":
                        this.inputId = true;
                        this.inputMsg = true;
                        return  "編輯工單";
                    case "delete":
                        this.inputId = true;
                        this.inputMsg = false;
                        return "刪除工單";
                }
            },
            getMsg(){
                axios.get('https://localhost:7185/api/TestLog')
                    .then(response => {
                        this.result = response.data;   
                        
                    }).catch( (error) => console.log(error));
            },
            getImg(){
                axios.get('https://localhost:7185/api/Image')
                    .then(response => {
                        this.result2 = response.data;
                        console.log("我是result2:"+this.result2[0].path);
                    }).catch( (error) => console.log(error));
            },
            send(){
                switch(this.$route.query.permission){
                    case "create":
                        axios.post('https://localhost:7185/api/TestLog',{logMsg:this.msg})
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "edit":
                        axios.put('https://localhost:7185/api/TestLog/'+this.id.trim(),{
                            id:this.id.trim(),
                            logMsg:this.msg
                        })
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "delete":
                        axios.delete('https://localhost:7185/api/TestLog/'+this.id.trim())
                            .then((response) => {
                                this.isPopup = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                }
                
            },
            changeFile(e:any){
                this.imgFile = e.target.files[0];
                console.log("我是file:"+this.imgFile);
            },
            sendImg(){
                let File = new FormData();
                switch(this.$route.query.permission){
                    case "create":
                        File.append("File", this.imgFile);
                        axios.post('https://localhost:7185/api/Image',File)
                            .then((response) => {
                                this.isPopup2 = false;
                                this.getImg();
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "edit":
                        File.append("File", this.imgFile);
                        console.log("edit"+this.imgFile);
                        axios.put('https://localhost:7185/api/Image/'+this.id.trim(),File)
                            .then((response) => {
                                this.isPopup2 = false;
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                    case "delete":
                        axios.delete('https://localhost:7185/api/Image/'+this.id.trim())
                            .then((response) => {
                                this.isPopup2 = false;
                                this.getImg();
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                            break;
                }
            },

            close() {
                this.isPopup = false
            },
            click() {
                this.isPopup = !this.isPopup
            },
            cancel() {
                this.isPopup = false
            },
            closeImg() {
                this.isPopup2 = false
            },
            clickImg() {
                this.isPopup2 = !this.isPopup2
            },
            cancelImg() {
                this.isPopup2 = false
            },
        }
    });
    
  </script>
  
  <style>
    .tableDiv{
        text-align: center;
    }
    .table{
        margin-left:43.5%;
        padding: 1%;
    }
    .table2{
        margin-left:10%;
        padding: 1%;
    }
    td{
        border:1px solid black;
    }



    .vue-popUpSmall {
        background-color: #e6e6e6;
        border: 4px solid #b9b9b9;
        color: #000000;
        padding: 5px;
        z-index: 1;
    }

    .vue-input {
        background-color: #e6e6e6;
        border: 4px solid #b9b9b9;
        color: #000000;
        padding: 5px;
        z-index: 1;
    }

    .button {
    display: inline;
    margin: 2px;
    }
  </style>
  