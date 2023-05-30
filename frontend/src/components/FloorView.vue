<template>
    <div class="tool_bar">    
        <p class="date_picker_label">Pick a date:</p>
        <VueDatePicker class="date_picker" v-model="date" :enable-time-picker="false"></VueDatePicker>                 
        <div ref="slider" class="slider">            
            <p id="start_time" class="start_time">7:00</p>
            <input id="ex16b" type="text"/>
            <p id="end_time" class="end_time">17:00</p>
        </div>
        <button id="button_find1" class="button_find">Find</button>        
    </div>    
    <div class="search_area">          
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
            rooms: [
                {id: "R1", available: 1, width: 180, height: 90, top: 10, left: 10, coordinates: [{x: 0, y:0}, {x:150, y:0}, {x: 180, y:90}, {x: 0, y:90}]},
                {id: "R2", available: 1, width: 60, height: 90, top: 10, left: 220, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}]},
                {id: "R3", available: 0, width: 60, height: 90, top: 10, left: 310, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}]},
                {id: "R4", available: 0, width: 180, height: 60, top: 150, left: 10, coordinates: [{x: 0, y:0}, {x:180, y:0}, {x: 180, y:60}, {x: 0, y:150}]},
                {id: "R5", available: 1, width: 150, height: 150, top: 150, left: 220, coordinates: [{x: 90, y:0}, {x:150, y:0}, {x: 150, y:150}, {x: 0, y:150}, {x: 0, y:90}, {x: 90, y:90}]},
                {id: "R6", available: -1, width: 60, height: 90, top: 10, left: 400, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:60, y:90}, {x:60, y:0}, {x:0, y:90}]},
                {id: "R7", available: -1, width: 180, height: 60, top: 240, left: 10, coordinates: [{x: 0, y:0}, {x:180, y:0}, {x: 180, y:60}, {x: 0, y:60}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:180, y:60}, {x: 180, y:0}, {x:0, y:60}]},
                {id: "R8", available: -1, width: 60, height: 60, top: 150, left: 220, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:60}, {x: 0, y:60}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:60, y:60}, {x:60, y:0}, {x:0, y:60}]},
            ],
            date: new Date()
        }
    },  
    mounted() {
        this.drawRooms();
        this.createTimeSlider();
        this.setupButtons();
    },
    methods: {
        drawRooms() {  
            this.rooms.forEach((room) => {
                $('#' + room.id).remove();
            })                
            this.rooms.forEach((room) => {
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
                if (room.available >= 0) {
                    if (room.available == 1) {
                        ctx.fillStyle = "#6db193";
                        canvas.addEventListener("mouseover", () => this.changeRoomsColor(room.id));
                        canvas.addEventListener("mouseleave", () => this.setRoomsDefaultColor(room.id));                         
                    } else {
                        ctx.fillStyle = "#DC143C";
                    }
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
                const container = this.$refs.room_container;
                const rect = container.getBoundingClientRect();                    
                canvas.style.position = 'absolute';
                canvas.style.top = rect.top + room.top + 'px';
                canvas.style.left = rect.left + room.left + 'px';                 
            })    
            var floorId = document.getElementById("floor_id");
            floorId.innerText = "Floor: " + this.currentFloor;  
        },
        setRoomsDefaultColor(id) {
            var canvas = document.getElementById(id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#6db193";
            ctx.fill();
        },
        changeRoomsColor(id) {
            var canvas = document.getElementById(id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#3a9679";
            ctx.fill();
        },   
        createTimeSlider() {
            var s = new Slider("#ex16b", { id: 'ex16b', min: 420, max: 1020, step: 5, value: [420, 1020], tooltip: 'hide'});            
            s.on("slide", function(values) {
                var hour = Math.trunc(values[0] / 60);
                var minute = values[0] % 60;
                if (minute < 10) {
                    minute = '0' + minute;
                }
                document.getElementById("start_time").innerHTML = hour + ":" + minute;
                hour = Math.trunc(values[1] / 60);
                minute = values[1] % 60;
                if (minute < 10) {
                    minute = '0' + minute;
                }
                document.getElementById("end_time").innerHTML = hour + ":" + minute;
            });  
        },
        setupButtons() {
            const container = this.$refs.room_container;
            const rect = container.getBoundingClientRect();                    
            const button_before = document.getElementById("button_before")
            button_before.style.position = 'absolute';
            button_before.style.top = rect.top + 330 + 'px';
            button_before.style.left = rect.left + 350 + 'px';
            button_before.addEventListener('click', () => this.decreaseFloorNumber());

            const button_next = document.getElementById("button_next")
            button_next.style.position = 'absolute';
            button_next.style.top = rect.top + 330 + 'px';
            button_next.style.left = rect.left + 473 + 'px';
            button_next.addEventListener('click', () => this.increaseFloorNumber());

            const floor_id = document.getElementById("floor_id");
            floor_id.style.position = 'absolute';
            floor_id.style.top = rect.top + 329 + 'px';
            floor_id.style.left = rect.left + 427 + 'px';

        },
        increaseFloorNumber() {
            if (this.currentFloor < 3) {
                this.currentFloor++
                var floorId = document.getElementById("floor_id");
                floorId.innerText = "Floor: " + this.currentFloor;
                this.drawRooms()
            }
        },
        decreaseFloorNumber() {
            if (this.currentFloor > 1) {
                this.currentFloor--
                var floorId = document.getElementById("floor_id");
                floorId.innerText = "Floor: " + this.currentFloor;
                this.drawRooms()
            }
        }
    }
  
};
</script>
<style src="@vueform/multiselect/themes/default.css"></style>
<style>
.tool_bar {
    width: 810px;
    height: 90px;
    padding: 0px;    
    border: 1px solid rgb(222, 222, 222);
}
.search_area {
    width: 270px;
    height: 360px;
    padding: 0px;
    border: 1px solid rgb(222, 222, 222);
    display: block;
    float:left;
    
}
.floor_view {
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
    margin-right: 20px;
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
    text-decoration: none;
    user-select: none;
    -webkit-user-select: none;
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