<template> 
<div id="app" class="small-container">
  <h3><b>ROOM RESERVATION SYSTEM</b></h3>
  <nav class="navbar bg-dark navbar-dark navbar-expand-sm" ref="navbar_logged_in">
      <div class="container">
        <ul class="navbar-nav mr-auto">
          <li id="floor_view" class="nav-item active" @click="setAsActiveTab(0)">
            <router-link to="/" class="nav-link">Floor view</router-link>
          </li>
          <li id="reservations" class="nav-item" @click="setAsActiveTab(1)">
            <router-link to="/reservations" class="nav-link">Reservations</router-link>
          </li>
        </ul>
      </div>     
      <div class="dropdown">
        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Welcome Michał
        </button>
        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
          <a class="dropdown-item" href="#">My Profile</a>
          <a class="dropdown-item" href="#" @click="changeLoginStatus(0)">Log Out</a>
        </div>
      </div>
  </nav>  
  <div ref="router_view" class="router_view">
    <router-view></router-view>
  </div>
  <div ref="login_form" class="login_form">
    <LoginForm @log-to-app="changeLoginStatus"/>
  </div>
</div>
</template>
<script>
export default {
  name: 'App',
  data() {
    return {
      user_logged_in: 0,
    }
  },
  methods: {
      setAsActiveTab(tabIndex){
          if(tabIndex == 0) {
            document.getElementById("floor_view").classList.add('active');
            document.getElementById("reservations").classList.remove('active');
          }
          else if(tabIndex == 1){          
            document.getElementById("reservations").classList.add('active');
            document.getElementById("floor_view").classList.remove('active');
          }
      },
      changeLoginStatus(status) {        
        this.user_logged_in = parseInt(status);
        if (this.user_logged_in == 1) {
          this.$refs.navbar_logged_in.style.display = "block";
          this.$refs.router_view.style.display = "block";
          this.$refs.login_form.style.display = "none";
        } else {
          this.$refs.navbar_logged_in.style.display = "none";
          this.$refs.router_view.style.display = "none";
          this.$refs.login_form.style.display = "block";   
        }
      },    
      
  },
  mounted() {
        this.setAsActiveTab(0)
        this.changeLoginStatus(0)              
  },
}
</script>
<style>
.small-container {
  position: absolute;
  top: 50%;
  left: 50%;
  margin-right: -50%;
  transform: translate(-50%, -50%);     
}
.router_view {
  width: 812px;
  height: 451px;
  border: 1px solid rgb(222, 222, 222);
}
.login_form {
  width: 812px;
}
.dropdown {
  position: absolute;
  top: 18%;
  right: 2%;
}
body {
  background-color: rgb(206, 198, 198);
}
.google-signin-button {
  color: white;
  background-color: red;
  height: 50px;
  font-size: 16px;
  border-radius: 10px;
  padding: 10px 20px 25px 20px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
</style>


