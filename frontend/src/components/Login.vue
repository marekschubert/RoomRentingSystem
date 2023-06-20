<template>
    <div>
        {{ port }}
    </div>
    <div class="login_page">       
        <div ref="login_form" class="login_form">
            <h2 style="margin: 7%;">Welcome back</h2>                                        
            <h4>Login:</h4>
            <GoogleLogin :callback="loginCallback"/>            
        </div>
    </div>
</template>
    
<script>
import { decodeCredential } from 'vue3-google-login'
export default {
    name: 'login-form',
    data() {
    return {
        port: process.env.BACKEND_PORT
    }
    },
    methods: {
        async loginCallback(response){            
            const userData = decodeCredential(response.credential);   
            var user = {"firstName": userData.given_name, "lastName": userData.family_name, "email": userData.email};
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(user)                
            };
            const res = await fetch("http://localhost:43023/api/user", requestOptions);
            localStorage.FirstName = userData.given_name;
            localStorage.LastName = userData.family_name;
            localStorage.Email = userData.email;       
            localStorage.user_logged_in = 1; 
            localStorage.activeTab = 0;
            this.$router.push("/").then(() => { this.$router.go() });                
        },
    },         
    mounted() {
        const myVariable = process.env.BACKEND_PORT;
        console.log(myVariable);
    }   
}
</script>

<style>
.login_page {
    height: 100%;    
    display: flex;    
    justify-content: center;
    align-items: center;
    font-size: 80%;
    background-color: white;
}
.login_form {
    height: 100%;    
    width: 100%;    
    text-align: center;
    margin-left: auto;
    margin-right: auto;    
}
</style>