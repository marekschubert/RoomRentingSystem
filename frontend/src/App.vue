<template>  
<div id="app" class="small-container">
  <h3><b>ROOM RESERVATION SYSTEM</b></h3>
  <nav class="navbar bg-dark navbar-dark navbar-expand-sm">
      <div class="container">
        <ul class="navbar-nav mr-auto">
          <li id="floor_view" class="nav-item active">
            <router-link class="nav-link">Floor view</router-link>
          </li>
          <li id="my_reservation" class="nav-item">
            <router-link class="nav-link">My reservations</router-link>
          </li>
          <li id="invitations" class="nav-item">
            <router-link class="nav-link">Invitations</router-link>
          </li>
        </ul>
      </div>
    </nav>
  <FloorView/>
</div>

</template>

<script>
import FloorView from './components/FloorView.vue'

export default {
  name: 'App',
  components: {
    FloorView
  }
}
</script>

<style>
.small-container {
  max-width: 810px; 
  margin:30px;
  }
</style>
