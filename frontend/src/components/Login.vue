<template>
    <div class="login_form">
        <form>    
            <h3 style="margin: 35%;">Login</h3>            
            <div class="form-outline mb-4">
                <input type="email" id="form2Example1" class="form-control" />                
                <label class="form-label" for="form2Example1">Email address</label>
            </div>

            <div class="form-outline mb-4">
                <input type="password" id="form2Example2" class="form-control" />
                <label class="form-label" for="form2Example2">Password</label>
            </div>

            <div class="row mb-4">
                <div class="col d-flex justify-content-center">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" value="" id="form2Example31" checked />
                    <label class="form-check-label" for="form2Example31"> Remember me </label>
                </div>
                </div>

                <div class="col">            
                <a href="#!">Forgot password?</a>
                </div>
            </div>
            
            <button type="button" class="btn btn-primary btn-block mb-4" @click="logToApp(1)">Sign in</button>

            <div class="text-center">
                <p>Not a member? <a href="#!">Register</a></p>
                <p>or sign up with:</p>
                <!--<button type="button" class="btn btn-link btn-floating mx-1">
                    <i class="fab fa-google-plus-g"></i>                    
                </button>-->
                <GoogleLogin :callback="callback"/>
            </div>
        </form>
    </div>
</template>
    
<script>
import { decodeCredential } from 'vue3-google-login'
import { googleLogout } from "vue3-google-login"
export default {
    name: 'login-form',
    methods: {
        logToApp(status) {                        
            this.$emit('log-to-app', status);                
        },
        callback(response){
            const userData = decodeCredential(response.credential)
            console.log("Handle the userData", userData)
            this.$emit('log-to-app', 1);  
        },
    }
}
</script>
    
<style>
.login_form {
    height: 100%;    
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 80%;
    background-color: white;
}
</style>