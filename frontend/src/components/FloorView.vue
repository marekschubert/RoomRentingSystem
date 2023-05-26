<template>
    <div class="tool_bar">       
    </div>    
    <div class="search_area">        
    </div>
    <div class="floor" ref="room_container" id="room_container"></div>
</template>

<script>
import $ from 'jquery'
//import axios from 'axios';
//import xml2js from 'xml2js';
export default {
    data() {
        return {
            currentFloor: 1,
            rooms: [
                {id: "R1", available: 1, width: 180, height: 90, top: 10, left: 10, coordinates: [{x: 0, y:0}, {x:150, y:0}, {x: 180, y:90}, {x: 0, y:90}]},
                {id: "R2", available: 1, width: 60, height: 90, top: 10, left: 220, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}]},
                {id: "R3", available: 0, width: 60, height: 90, top: 10, left: 310, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}]},
                {id: "R4", available: 1, width: 180, height: 60, top: 150, left: 10, coordinates: [{x: 0, y:0}, {x:180, y:0}, {x: 180, y:60}, {x: 0, y:150}]},
                {id: "R5", available: 1, width: 150, height: 150, top: 150, left: 220, coordinates: [{x: 90, y:0}, {x:150, y:0}, {x: 150, y:150}, {x: 0, y:150}, {x: 0, y:90}, {x: 90, y:90}]},
                {id: "R6", available: -1, width: 60, height: 90, top: 10, left: 400, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:90}, {x: 0, y:90}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:60, y:90}, {x:60, y:0}, {x:0, y:90}]},
                {id: "R7", available: -1, width: 180, height: 60, top: 240, left: 10, coordinates: [{x: 0, y:0}, {x:180, y:0}, {x: 180, y:60}, {x: 0, y:60}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:180, y:60}, {x: 180, y:0}, {x:0, y:60}]},
                {id: "R8", available: -1, width: 60, height: 60, top: 150, left: 220, coordinates: [{x: 0, y:0}, {x:60, y:0}, {x: 60, y:60}, {x: 0, y:60}, {x: 0, y:0}], crosses: [{x:0, y:0}, {x:60, y:60}, {x:60, y:0}, {x:0, y:60}]},
            ]
        }
    },  
    mounted() {
        this.draw();
    },
    methods: {
        draw() {
            this.rooms.forEach((room) => {
                const canvas = document.createElement("canvas");
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
                        canvas.addEventListener("mouseover", () => this.changeColor(room.id));
                        canvas.addEventListener("mouseleave", () => this.setDefaultColor(room.id));
                        $(room.id).addClass("pointer");      
                    } else {
                        ctx.fillStyle = "#DC143C";
                    }
                    ctx.fill();
                    this.$refs.room_container.appendChild(canvas);
                    const container = this.$refs.room_container;
                    const rect = container.getBoundingClientRect();
                    $(room.id).css({
                        position:'absolute',
                        top: rect.top + room.top,
                        left: rect.left + room.left
                    });                       
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
                    this.$refs.room_container.appendChild(canvas);
                }
            })
            
        },
        setDefaultColor(id) {
            var canvas = document.getElementById(id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#6db193";
            ctx.fill();
        },
        changeColor(id) {
            var canvas = document.getElementById(id);
            var ctx = canvas.getContext("2d"); 
            ctx.fillStyle = "#3a9679";
            ctx.fill();
        },   
    }
  
};
</script>

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
.floor {
    width: 540px;
    height: 360px;
    padding: 0px;
    background-color:rgb(229, 227, 227);    
    display: block;
    float:left;
    
}
.pointer {
    cursor: pointer;
}
</style>