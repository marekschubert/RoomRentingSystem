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
        </button>
        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">          
          <a class="dropdown-item" href="#" @click="changeLoginStatus(0)">Log Out</a>
        </div>
      </div>
  </nav>  
  <div ref="router_view" class="router_view">
    <router-view @set-as-active-tab="setAsActiveTab" @reserve-room="reserveRoom"></router-view>
  </div>    
  <div ref="reservation_page" class="reservation_page">
    <RoomReservation :key="reservationComponentKey" @return-to-floor-view="returnToFloorView"/>
  </div>
</div>
</template>
<script>
import RoomReservation from './components/Reservation.vue';
export default {
  name: 'App',
  components: {RoomReservation},
  data() {
    return {
      loginComponentKey: 0,
      reservationComponentKey: 0
    }
  },
  methods: {
      setAsActiveTab(tabIndex){
        if (tabIndex == null) {
          tabIndex = 0;
          localStorage.activeTab = 0;
        } else {
          localStorage.activeTab = tabIndex;
        }
        if(tabIndex == 0) {
          document.getElementById("floor_view").classList.add('active');
          document.getElementById("reservations").classList.remove('active');
        }
        else if(tabIndex == 1){          
          this.returnToFloorView();
          document.getElementById("reservations").classList.add('active');
          document.getElementById("floor_view").classList.remove('active');          
        }
      },
      changeLoginStatus(status) {    
        localStorage.user_logged_in = parseInt(status);                
        if (localStorage.user_logged_in == 1) {                    
          this.$refs.navbar_logged_in.style.display = "block";
          this.$refs.router_view.style.display = "block";    
          this.$refs.reservation_page.style.display = "none";                
          document.getElementById("dropdownMenuButton").innerHTML = "Welcome " + localStorage.FirstName;
        } else {
          this.$refs.navbar_logged_in.style.display = "none";
          this.$refs.reservation_page.style.display = "none";   
          this.$refs.router_view.style.display = "block";                 
          this.$router.push('/login');
        }
      },
      reserveRoom() {
        this.$refs.reservation_page.style.display = "block";      
        this.$refs.router_view.style.display = "none";
        this.reservationComponentKey += 1;
      },
      returnToFloorView() {
        this.$refs.reservation_page.style.display = "none";      
        this.$refs.router_view.style.display = "block";
      },            
  },
  mounted() {
        if (localStorage.user_logged_in === null) {
          localStorage.user_logged_in = 0;
        }
        this.changeLoginStatus(localStorage.user_logged_in);       
        this.setAsActiveTab(localStorage.activeTab);             
        
        if (localStorage.myReservations == null) {
          var myReservations = [{RoomId: 1, FloorId: 1, StartDateTime: "2023-06-16 7:00", EndDateTime: "2023-06-16 9:15", Participants: ["Hien Le", "Marek Schubert"]},
                                {RoomId: 15, FloorId: 2, StartDateTime: "2023-07-03 15:10", EndDateTime: "2023-07-03 16:15", Participants: ["Hien Le", "Adam Kowalik"]}
                              ]
          localStorage.myReservations = JSON.stringify(myReservations);
        }
        this.$refs.reservation_page.style.display = "none";       
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
  background-color: white;
}
.reservation_page {
  width: 812px;
  height: 451px;  
  background-color: white;
}
.login_page {
  width: 812px;
  text-align: center;
  margin-left: auto;
  margin-right: auto;
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


