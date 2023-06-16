<template>
    <div class="login_page">
        <form ref="register_form">    
            <p>Your account is not registered in the system</p>   
            <h3 style="margin: 10%;">Register</h3>                     
            <div class="form-outline mb-4">
                <label class="form-label" for="formFirstName">First name</label>
                <input type="text" id="formFirstName" class="form-control"/><br>
                <label class="form-label" for="formLastName">Last name</label>
                <input type="text" id="formLastName" class="form-control" /><br>
                <p>Email: {{this.Email}}</p>                
            </div>
            <button type="button" class="btn btn-primary btn-block mb-4" @click="signUp">Sign up</button>
        </form>
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
        FirstName: "",
        LastName: "",
        Email: "",
    }
    },
    methods: {
        signUp() {                        
            this.FirstName = document.getElementById("formFirstName").value;
            this.LastName = document.getElementById("formLastName").value;
            var users = JSON.parse(localStorage.users);            
            users.push({FirstName: this.FirstName, LastName: this.LastName, Email: this.Email});
            localStorage.users = JSON.stringify(users);        
            localStorage.FirstName = this.FirstName;
            localStorage.LastName = this.LastName;
            localStorage.Email = this.Email;    
            localStorage.user_logged_in = 1;
            localStorage.activeTab = 0;
            this.$router.push("/").then(() => { this.$router.go() });    
        },
        loginCallback(response){
            const userData = decodeCredential(response.credential);            
            const users = JSON.parse(localStorage.users)    
            console.log(userData)        
            var email_exists = false
            var existing_user = null;            
            users.forEach((user) => {                
                if (user.Email === userData.email) {
                    email_exists = true;
                    existing_user = user;
                }
            })
            if (email_exists) {
                localStorage.FirstName = existing_user.FirstName;
                localStorage.LastName = existing_user.LastName;
                localStorage.Email = existing_user.Email;
                localStorage.user_logged_in = 1; 
                localStorage.activeTab = 0;
                this.$router.push("/").then(() => { this.$router.go() });                
            } else {
                this.FirstName = userData.given_name;
                this.LastName = userData.family_name;
                this.Email = userData.email;
                document.getElementById("formFirstName").value = this.FirstName;
                document.getElementById("formLastName").value = this.LastName;
                this.$refs.login_form.style.display = "none";
                this.$refs.register_form.style.display = "block";
            }
        },
    },         
    mounted() {
        this.$refs.register_form.style.display = "none";        
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