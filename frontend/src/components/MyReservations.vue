<template>
    <div class="reservation_list">
        <div class="reservation_item header_list">
            <p class="organizer header_item">Organizer</p>
            <p class="status header_item">Status</p>
            <p class="date header_item">Date</p>
            <p class="time header_item">Time</p>
            <p class="room header_item">Room</p>
            <p class="floor header_item">Floor</p>
            <p class="participants header_item">Participants</p>
        </div>
        <div class="reservation_item">
            <p class="organizer">Hien Le</p>
            <p class="status">Active</p>
            <p class="date">2023-06-16</p>
            <p class="time">15:00 - 16:00</p>
            <p class="room">15</p>
            <p class="floor">2</p>
            <p class="participants" type="button" href="#">Click to see</p>
        </div>
        <div class="reservation_item">Reservation 3</div>
        <div class="reservation_item">Reservation 4</div>
        <div class="reservation_item">Reservation 5</div>
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

    },
    mounted() {        
        
    }
}
</script>
<style>
.reservation_list {
    height: 100%;
    display: flex;
    flex-wrap: wrap;
    align-items: flex-start;
    align-content: flex-start;
    background-color: rgb(240, 243, 246);
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
    margin-top: 2%;
    border-radius: 5px;

}
.header_item{
    font-size: large;
    font-weight: bold;        
}
.organizer {        
    width: 14%;
}
.status {
    width: 10%;
}
.date {
    width: 14%;
}
.time {
    width: 16%;
}
.room {
    width: 10%;
}
.floor {
    width: 10%;
}
.reservation_item .normal_item {
    font-weight: normal;
}
</style>