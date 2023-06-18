<template>
    <div class="tool_bar">    
        <p class="date_picker_label">Pick a date:</p>
        <VueDatePicker class="date_picker" v-model="date" :enable-time-picker="false"></VueDatePicker>                 
        <div ref="slider" class="slider">            
            <p id="start_time" class="start_time">07:00</p>
            <input id="ex16b" type="text"/>
            <p id="end_time" class="end_time">17:00</p>
        </div>
        <button id="button_find1" class="button_find" @click="drawRooms">Find</button>        
    </div>    
    <div class="room_info_area">   
        <h4>Room information</h4>   
        <p ref="room_number"></p>       
        <p ref="room_availability"></p> 
        <p ref="room_capacity"></p>     
        <button ref="reserve_room_btn" class="button_find" style="display: none;" @click="reserveRoom">Reserve</button>
        <p ref="room_reserved_times"></p>
    </div>
    <div class="floor_view" ref="room_container" id="room_container">  
        <button id="button_before" class="button_find">&#60;</button>
        <p id="floor_id">Floor: 1</p>
        <button id="button_next" class="button_find">&#62;</button>
    </div>    
</template>
<script>
import $ from 'jquery'
import Slider from 'bootstrap-slider'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
export default {
    name: 'floor-view',
    components: { VueDatePicker },
    data() {
        return {
            currentFloor: 1,  
            currentRoom: null,          
            layers: [],
            rooms: [],
            date: new Date(),
            startDT: new Date(),
            endDT: new Date()
        }
    },  
    mounted() {
        this.getRoomData();
        this.createTimeSlider();
        this.setupButtons();
        this.drawRooms();                
    },
    methods: {
        async getRoomData() {
            if (localStorage.roomData == null) {
                await fetch("https://localhost:44346/api/Room")
                        .then((response) => response.json())
                        .then((data) => {
                            localStorage.roomData = JSON.stringify(data);                                    
                        });
            }            
        },
        async drawRooms() {    
            this.clearRoomsDetails();
            const roomData = JSON.parse(localStorage.roomData);            
            this.layers = roomData.layers;                    
            this.layers.forEach((layer) => {                
                layer.rooms.forEach((room) => {                    
                    $('#' + room.id).remove();                    
                    if (room.reservable != -1) {
                        room.reservable = 1;
                    }
                })
            })        
            this.rooms = this.layers[this.currentFloor - 1].rooms;                    
            this.startDT = new Date(this.date);    
            var startTime = document.getElementById("start_time").innerHTML;            
            this.startDT.setHours(startTime.substring(0, 2), startTime.substring(3, 5), 0);  
            
            this.endDT = new Date(this.date);    
            var endTime = document.getElementById("end_time").innerHTML;
            this.endDT.setHours(endTime.substring(0, 2), endTime.substring(3, 5), 0);  
            var reservations;  
            
            await fetch("https://localhost:44346/api/reservation")
                    .then((response) => response.json())
                    .then((data) => {
                        reservations = data;                                    
                    });
            var reservationStartDT, reservationEndDT;
            reservations.forEach((item) => {
                reservationStartDT = new Date(item.reservationData.startDateTime);
                reservationEndDT = new Date(item.reservationData.endDateTime);       
                if (reservationStartDT < this.endDT && reservationEndDT > this.startDT) {                    
                    this.rooms.forEach((room) => {
                        if (room.id == item.roomData.id) {
                            room.reservable = 0;
                            if (room.reservedTimes == null) {
                                room.reservedTimes = []
                            }
                            room.reservedTimes.push(item.reservationData.startDateTime.substring(11, 16) + ' - ' + item.reservationData.endDateTime.substring(11, 16));                            
                        }
                    })
                }                
            });                 
            this.rooms.forEach((room) => {
                if (room.coordinates != undefined) {                                    
                    const canvas = document.createElement("canvas");
                    canvas.setAttribute('ref', room.id);
                    canvas.id = room.id;
                    canvas.width = room.width;
                    canvas.height = room.height;                      
                    var ctx = canvas.getContext("2d"); 
                    ctx.beginPath();                
                    var firstPoint = false;
                    room.coordinates.forEach(coordinate => {                    
                        if (firstPoint == false) {
                            ctx.moveTo(coordinate.x, coordinate.y);
                            firstPoint = true;
                        } else {
                            ctx.lineTo(coordinate.x, coordinate.y);
                        }
                    })
                    if (room.reservable >= 0) {
                        canvas.room = room;
                        if (room.reservable == 1) {
                            ctx.fillStyle = "#6db193";
                            canvas.addEventListener("mouseover", this.changeRoomsColor);
                            canvas.addEventListener("mouseleave", this.setRoomsDefaultColor);
                        } else {
                            ctx.fillStyle = "#DC143C";                            
                        }
                        canvas.addEventListener("click", this.displayRoomsDetails);
                        ctx.fill();      
                        canvas.style.cursor = 'pointer';                             
                    } else {                    
                        var movedTo = false;
                        room.crosses.forEach(cross => {
                            if (!movedTo) {
                                ctx.moveTo(cross.x, cross.y);
                                movedTo = true;
                            } else {
                                ctx.lineTo(cross.x, cross.y);
                                movedTo = false;
                            }
                        })
                        ctx.stroke();
                    }
                    this.$refs.room_container.appendChild(canvas);                                         
                    canvas.style.position = 'absolute';
                    canvas.style.top = room.top + 'px';
                    canvas.style.left = room.left + 'px';       
                }          
            })    
            var floorId = document.getElementById("floor_id");
            floorId.innerText = "Floor: " + this.currentFloor;  
        },
        setRoomsDefaultColor(evt) {
            var canvas = document.getElementById(evt.currentTarget.room.id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#6db193";
            ctx.fill();
        },
        changeRoomsColor(evt) {
            var canvas = document.getElementById(evt.currentTarget.room.id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#3a9679";
            ctx.fill();
        },   
        displayRoomsDetails(evt) {
            if (this.currentRoom != null && this.currentRoom.reservable == 1) {
                var canvas = document.getElementById(this.currentRoom.id);
                canvas.addEventListener("mouseover", this.changeRoomsColor);
                canvas.addEventListener("mouseleave", this.setRoomsDefaultColor);  
                canvas.dispatchEvent(new Event("mouseleave"));
            }
            var pickedRoom = evt.currentTarget.room;
            if (pickedRoom.reservable == 1) {
                canvas = document.getElementById(pickedRoom.id);
                canvas.dispatchEvent(new Event("mouseover"));
                canvas.removeEventListener("mouseover", this.changeRoomsColor);
                canvas.removeEventListener("mouseleave", this.setRoomsDefaultColor);           
            } 
            this.currentRoom = pickedRoom;
            this.$refs.room_number.innerHTML = "Number: " + pickedRoom.id;
            this.$refs.room_capacity.innerHTML = "Capacity: " + pickedRoom.capacity + " persons";
            if (pickedRoom.reservable == 1) {
                this.$refs.room_availability.innerHTML = "Reservable: Yes";
                this.$refs.reserve_room_btn.style.display = "block";
                this.$refs.room_reserved_times.innerHTML = "";
            } else {
                this.$refs.room_availability.innerHTML = "Reservable: No";
                this.$refs.reserve_room_btn.style.display = "none";
                this.$refs.room_reserved_times.innerHTML = "Reserved times: <br>";
                this.currentRoom.reservedTimes.forEach((time) => {
                    this.$refs.room_reserved_times.innerHTML += "&emsp;" + time + "<br>";
                });
            }
        },
        clearRoomsDetails() {
            this.$refs.room_number.innerHTML = "";
            this.$refs.room_capacity.innerHTML = "";
            this.$refs.room_availability.innerHTML = "";        
            this.$refs.reserve_room_btn.style.display = "none";
            this.$refs.room_reserved_times.innerHTML = "";
        },
        createTimeSlider() {
            var s = new Slider("#ex16b", { id: 'ex16b', min: 420, max: 1020, step: 5, value: [420, 1020], tooltip: 'hide'});            
            s.on("slide", function(values) {
                var hour = Math.trunc(values[0] / 60);
                var minute = values[0] % 60;
                if (hour < 10) {
                    hour = '0' + hour;
                }
                if (minute < 10) {
                    minute = '0' + minute;
                }
                document.getElementById("start_time").innerHTML = hour + ":" + minute;
                hour = Math.trunc(values[1] / 60);
                minute = values[1] % 60;
                if (hour < 10) {
                    hour = '0' + hour;
                }
                if (minute < 10) {
                    minute = '0' + minute;
                }
                document.getElementById("end_time").innerHTML = hour + ":" + minute;
            });  
        },
        setupButtons() {                         
            const button_before = document.getElementById("button_before")
            button_before.style.position = 'absolute';
            button_before.style.bottom = '2px';
            button_before.style.right = '120px';
            button_before.addEventListener('click', () => this.decreaseFloorNumber());

            const button_next = document.getElementById("button_next")
            button_next.style.position = 'absolute';
            button_next.style.bottom = '2px';
            button_next.style.right = '2px';
            button_next.addEventListener('click', () => this.increaseFloorNumber());

            const floor_id = document.getElementById("floor_id");
            floor_id.style.position = 'absolute';
            floor_id.style.bottom = '-13px';
            floor_id.style.right = '55px';
        },
        increaseFloorNumber() {
            if (this.currentFloor < 3) {                
                this.currentFloor++;
                this.currentRoom = null;
                this.clearRoomsDetails();
                var floorId = document.getElementById("floor_id");
                floorId.innerText = "Floor: " + this.currentFloor;
                this.drawRooms()               
            }
        },
        decreaseFloorNumber() {
            if (this.currentFloor > 1) {
                this.currentFloor--;
                this.currentRoom = null;
                this.clearRoomsDetails();
                var floorId = document.getElementById("floor_id");
                floorId.innerText = "Floor: " + this.currentFloor;
                this.drawRooms()
            }
        },
        reserveRoom() {
            var reservationInfo = {"roomNumber": this.currentRoom.id,
                                    "floorNumber": this.currentFloor,
                                    "capacity": this.currentRoom.capacity,
                                    "date": this.date.toISOString().split('T')[0],
                                    "time": document.getElementById("start_time").innerHTML + " - " +
                                            document.getElementById("end_time").innerHTML,
                                    "startDT": this.startDT,
                                    "endDT": this.endDT};                                                                                  
            localStorage.reservationInfo = JSON.stringify(reservationInfo);            
            this.$emit('reserve-room');  
        }
    }
};
</script>
<style src="@vueform/multiselect/themes/default.css"></style>
<style>
.tool_bar {
    position: relative;
    height: 90px;
    padding: 0px;    
    border: 1px solid rgb(222, 222, 222);
    background-color: white;
}
.room_info_area {
    position: relative;
    width: 270px;
    height: 360px;
    padding: 0px;    
    display: block;
    float:left;
    padding: 5px 30px;
    line-height: 30px;
    font-size: small;
    background-color: white;
}
.floor_view {
    position: relative;
    width: 540px;
    height: 360px;
    padding: 0px;
    background-color:rgb(229, 227, 227);
    display: block;
    float:left;
}
.slider {
    display: inline-block;
}
.start_time {
    margin-left: 20px;
    margin-right: 25px;
    margin-top: 10px;
    width: 30px;
    display: inline-block;
}
.end_time {
    margin-left: 20px;
    width: 30px;
    display: inline-block;
}
#ex16b .slider-selection {
	background: orange;
}
.date_picker {
    width: 223px;
    display: inline-block;
}
.date_picker_label {
    margin: 20px;
    margin-top: 25px;
    display: inline-block;
    left: 200px;
}
.button_find {
    appearance: none;
    background-color: #3eb5f5;
    border: 1px solid rgba(27, 31, 35, .15);
    border-radius: 6px;
    box-shadow: rgba(27, 31, 35, .1) 0 1px 0;
    box-sizing: border-box;
    color: #fff;
    cursor: pointer;
    display: inline-block;  
    margin-left: 20px;    
    font-size: 14px;
    font-weight: 600;
    line-height: 20px;
    padding: 6px 16px;
    text-align: center;
    touch-action: manipulation;
    vertical-align: middle;
    white-space: nowrap;
}

.button_find:focus:not(:focus-visible):not(.focus-visible) {
    box-shadow: none;
    outline: none;
}

.button_find:hover {
    background-color: #1babf9;
}

.button_find:focus {
    box-shadow: #06a3f7 0 0 0 3px;
    outline: none;
}

.button_find:disabled {
    background-color: #3eb5f5;
    border-color: rgba(27, 31, 35, .1);
    color: rgba(255, 255, 255, .8);
    cursor: default;
}

.button_find:active {
    background-color: #03a2f7;
    box-shadow: rgba(20, 70, 32, .2) 0 1px 0 inset;
}

#button_before {
    height: 25px;
    padding: 0px 16px;
}

#button_next {
    height: 25px;
    padding: 0px 16px;
}
</style>