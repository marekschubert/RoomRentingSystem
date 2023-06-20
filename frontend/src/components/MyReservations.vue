<template>
    <div class="reservation_header">
        <p class="reservationID_header">ID</p>
        <p class="status_header">Status</p>
        <p class="date_header">Date</p>
        <p class="time_header">Time</p>
        <p class="room_header">Room</p>
        <p class="floor_header">Floor</p>
        <p class="participants_header">Participants</p>        
    </div>
    <div ref="reservation_list" class="reservation_list">
    </div>
</template>
<script>
export default {
    name: 'my-reservations',
    data() {
        return {

        };
    },  
    methods: {        
        async displayReservations() {
            var reservations;  
            await fetch("http://localhost:43023/api/reservation")
                    .then((response) => response.json())
                    .then((data) => {
                        reservations = data;                                    
                    });
            reservations.forEach((item) => {          
                var res_element = document.createElement("div");
                res_element.className = "reservation_item";

                var reservationID_element = document.createElement("p");
                reservationID_element.className = "reservationID";
                reservationID_element.innerHTML = item.reservationData.id;
                res_element.appendChild(reservationID_element);

                var status_element = document.createElement("p");
                status_element.className = "status";
                status_element.innerHTML = "Active";
                res_element.appendChild(status_element);

                var date_element = document.createElement("p");
                date_element.className = "date";
                date_element.innerHTML = item.reservationData.startDateTime.substring(0, 10);
                res_element.appendChild(date_element);

                var time_element = document.createElement("p");
                time_element.className = "time";
                time_element.innerHTML = item.reservationData.startDateTime.substring(11, 16) + ' - ' + item.reservationData.endDateTime.substring(11, 16);
                res_element.appendChild(time_element);

                var room_element = document.createElement("p");
                room_element.className = "room";
                room_element.innerHTML = item.roomData.id;
                res_element.appendChild(room_element);

                var floor_element = document.createElement("p");
                floor_element.className = "floor";
                floor_element.innerHTML = item.layerData.id;
                res_element.appendChild(floor_element);

                var collapse_element = document.createElement("div");
                collapse_element.id = "collapse" + item.reservationData.id;                
                collapse_element.className = "collapse";                
                collapse_element.innerHTML = "<b>Participant list: </b>";                
                item.participantsData.forEach((participant) => {
                    collapse_element.innerHTML += participant.firstName + ' ' + participant.lastName + ", "
                });
                collapse_element.innerHTML = collapse_element.innerHTML.slice(0, -2);

                var participants_element = document.createElement("a");
                participants_element.className = "participants";
                participants_element.innerHTML = "Click to see";
                participants_element.setAttribute("data-toggle", "collapse");                
                participants_element.href = "#" + collapse_element.id;
                participants_element.setAttribute("onclick", "if(this.innerHTML =='Click to see'){this.innerHTML = 'Hide'}else{this.innerHTML = 'Click to see'}");
                res_element.appendChild(participants_element);
                res_element.appendChild(collapse_element);

                this.$refs.reservation_list.appendChild(res_element);
            });     
        }
    },
    mounted() {        
        this.displayReservations();
    }
}
</script>
<style>
.reservation_header {
    height: fit-content;
    display: flex;
    flex-wrap: wrap;
    align-items: flex-start;
    align-content: flex-start;
    background-color: rgb(240, 243, 246);
    border: 0.5px solid rgb(219, 213, 213); 
    border-radius: 5px;
    margin: 10px;    
}
.reservation_header > * {
    font-size: large;
    font-weight: bold;    
    margin: 0px 10px 7px 20px;    
}
.reservation_list {
    height: 85%;
    display: flex;
    flex-wrap: wrap;
    align-items: flex-start;
    align-content: flex-start;
    overflow-y: scroll;
}
.reservation_item {    
    margin: 0px 10px 10px 10px;    
    width: 100%;
    height: fit-content;
    display: flex;
    flex-wrap: wrap;    
    border: 0.5px solid rgb(219, 213, 213);
    border-radius: 15px;
    background-color: white;
}
.reservation_item > *{
    padding: 2% 0% 0% 3%;    
    height: fit-content;
    font-size: small;
    font-weight: normal;        
}
.header_list {
    margin: 0px 10px 10px 10px;    
    border-radius: 5px;
}
.header_item{
    font-size: large;
    font-weight: bold;        
}
.reservationID {        
    width: 8%;
}
.reservationID_header {        
    width: 4%;
}
.status {
    width: 12%;
}
.status_header {
    width: 8%;
}
.date {
    width: 14%;
}
.date_header {
    width: 10%;
}
.time {
    width: 16%;
}
.time_header {
    width: 12%;
}
.room {
    width: 10%;
}
.room_header {
    width: 6%;
}
.floor {
    width: 10%;
}
.floor_header {
    width: 6%;
}
.participants {
    text-decoration: underline;
    width: 30%;
}
.reservation_item .normal_item {
    font-weight: normal;
}
</style>