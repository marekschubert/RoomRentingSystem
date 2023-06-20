<template>
    <div class="reservation_page">
        <h4>NEW RESERVATION:</h4>
        <div class="reservation_details">
            <p ref="room_number">Room: xxxxxxxx</p>
            <p ref="floor_number">Floor:</p>
            <p ref="capacity">Capacity:</p>
            <p ref="date">Date:</p>
            <p ref="time">Time:</p>
        </div>
        <div class="participant_list">
            <p ref="organizers_name">Organizer: </p>
            <p>Participants:</p>
            <Multiselect class="participant_multiselect"
                v-model="participantList"
                mode="tags"
                :close-on-select="false"
                :searchable="true"
                :create-option="true"
                :options="userList"
                />
        </div>
        <div class="button_area">
            <button type="button" class="btn btn-primary" style="background-color: rgb(47, 97, 212);" @click="confirmReservation">Confirm</button>
            <button type="button" class="btn btn-primary" style="background-color: rgb(173, 29, 29);" @click="cancelReservation">Cancel</button>
        </div>
    </div>    
</template>

<script>
import Multiselect from '@vueform/multiselect'

export default {
    name: 'room-reservation',
    components: {
      Multiselect,
    },
    data() {
        return {
            participantList: [],
            userList: [],
            startDT: new Date(),
            endDT: new Date(),
            roomId: 1
        };
    },  
    methods: {
        async updateUserList() {
            var users = []
            await fetch("http://localhost:43023/api/user")
            .then((response) => response.json())
            .then((data) => {
                data.forEach((user) => {                    
                    users.push({FirstName: user.firstName, LastName: user.lastName, Email: user.email});                                        
                })                                      
            });                
            localStorage.users = JSON.stringify(users);     
        },
        displayReservationInfo() {                                             
            if (localStorage.reservationInfo != null && localStorage.users != null) {
                const users = JSON.parse(localStorage.users);                       
                users.forEach((user) => {
                    if (user.Email != localStorage.Email) {
                        this.userList.push({value: user.Email, label: user.FirstName + " " + user.LastName});
                    }
                });
                const reservation = JSON.parse(localStorage.reservationInfo)                
                this.$refs.room_number.innerHTML = "Room: " + reservation.roomNumber;            
                this.$refs.floor_number.innerHTML = "Floor: " + reservation.floorNumber;
                this.$refs.capacity.innerHTML = "Capacity: " + reservation.capacity;
                this.$refs.date.innerHTML = "Date: " + reservation.date;
                this.$refs.time.innerHTML = "Time: " + reservation.time;
                this.$refs.organizers_name.innerHTML = "Organizer: " + localStorage.FirstName + ' ' + localStorage.LastName;
                this.startDT = new Date(reservation.startDT);
                this.startDT.setHours(this.startDT.getHours() + 2);
                this.endDT = new Date(reservation.endDT);
                this.endDT.setHours(this.endDT.getHours() + 2);
                this.roomId = reservation.roomNumber;
            }                          
        },
        async confirmReservation() {   
            this.participantList.push(localStorage.Email);
            console.log(this.participantList);
            var reservation = {"startDateTime": this.startDT,"endDateTime": this.endDT,"roomId": this.roomId,"participantsEmailAddresses": this.participantList};
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(reservation)                
            };
            const res = await fetch("http://localhost:43023/api/reservation", requestOptions);
            localStorage.activeTab = 1;
            this.$emit('set-as-active-tab', 1);                 
            this.$router.push("/reservations").then(() => { this.$router.go() });                
        },
        cancelReservation() {
            localStorage.activeTab = 0;
            this.$emit('set-as-active-tab', 0);                 
            this.$emit('return-to-floor-view');      
        },                
    },
    mounted() {
        this.updateUserList();
        this.displayReservationInfo();
    }
}
</script>
<style>
.reservation_page {        
    display: flex;
    flex-wrap: wrap;
}
.reservation_page > h4{        
    padding: 20px 0px 0px 20px;    
}
.reservation_details {
    padding: 10% 0%;
    display: block;
    width: 25%;
    font-size: small;
}
.participant_list {
    padding: 10% 0%;
    display: block;
    width: 40%;
    font-size: small;
}
.participant_multiselect {
    --ms-font-size: 10px;
    --ms-option-font-size: 10px;
    --ms-tag-font-size: 10px;
}
.button_area {
    display: block;
    text-align: center;
    width: 100%;
}
.button_area > * {
    margin-left: 10px;
}
</style>
<style src="@vueform/multiselect/themes/default.css"></style>
