<template> 
<div id="app" class="small-container">
  <h3><b>ROOM RESERVATION SYSTEM</b></h3>
  <nav class="navbar bg-dark navbar-dark navbar-expand-sm">
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
          <a class="dropdown-item" href="#">Log Out</a>
        </div>
      </div>
  </nav>
  <router-view/>
</div>
</template>
<script>
export default {
  name: 'App',
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
      }
    },
    mounted() {
      this.setAsActiveTab(0)
    },
}
</script>
<style>
.small-container {
    margin-top:30px;
    margin-left:300px;
    padding: 0px;     
    max-width: 810px;
}
.dropdown {
    position: absolute;
    top: 9px;
    left: 630px;
}
</style>


