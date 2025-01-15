<template>
  <div class="taskList">
    <div 
      v-for="(task, index) in tasks" 
      :key="index" 
      class="taskCard">
      <div class="cardHeader">
        <h3 class="taskTitle">{{ task.title }}</h3>
        <div class="taskOptions">
          <span class="taskStatus">{{ task.status }}</span>
          <button class="expandBtn" @click="toggleExpand(index)">
            <font-awesome-icon :icon="expandedIndex === index ? ['fas', 'chevron-up'] : ['fas', 'chevron-down']" />
          </button>
          <button class="openBaloonBtn" @click="toggleBalloon">
            <font-awesome-icon :icon="['fas', 'ellipsis-vertical']" />
          </button>
          <div v-if="isBalloonOpen" class="balloon">
            <p>C</p>
          </div>
        </div>
      </div>
      <div v-if="expandedIndex === index" class="cardDescription">
        <p>{{ task.id }}</p>
        <p>{{ task.description }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';

defineProps({
  tasks: {
    type: Array,
    required: true,
  },
});

const expandedIndex = ref(null);
const isBalloonOpen = ref(false);

const toggleBalloon = () => {
  isBalloonOpen.value = !isBalloonOpen.value;
};
const toggleExpand = (index) => {
  expandedIndex.value = expandedIndex.value === index ? null : index;
};
</script>

<style>
.taskList {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 16px;
}
  
.taskCard {
  display: flex;
  flex-direction: column;
  background-color: #ffffff;
  border-radius: 14px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  padding: 16px;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
  
.cardHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.taskTitle {
  font-size: 18px;
  font-weight: bold;
  margin: 0;
}

.taskStatus {
  font-size: 14px;
  font-weight: 500;
  color: #555;
}

.expandBtn {
  background: none;
  border: none;
  font-size: 20px;
  cursor: pointer;
  color: #007bff;
  padding: 4px;
  border-radius: 8px;
  transition: background-color 0.2s ease;
}
  
.expandBtn:hover {
  background-color: #f1f1f1;
}

.buttonMenu {
  border: none;
  background: transparent;
  cursor: pointer;
}

.cardDescription {
  margin-top: 12px;
  font-size: 14px;
  color: #333;
}
  
@media (max-width: 768px) {
  .taskCard {
    padding: 12px;
  }

  .taskTitle {
    font-size: 16px;
  }

  .taskStatus {
    font-size: 12px;
  }

  .cardDescription {
    font-size: 12px;
  }
}


.openBaloonBtn {
  position: absolute;
  width: 0;
  height: 0;
  padding: 0;
  border: none;
  font-size: 16px;
  cursor: pointer;
  position: relative; 
}

.balloon {
  position: absolute;
  right: 0%; /* Faz o balão abrir para cima do botão */
  top: 100%;
  left: 100%;
  transform: translateX(-50%);
  background-color: #f9f9f9;
  border: 1px solid #ccc;
  border-radius: 8px;
  padding: 10px;
  min-width: 10px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
  opacity: 0;
  visibility: hidden;
  transform: translateX(-50%) scale(0.8);
  transition: opacity 0.3s, transform 0.3s;
}

.balloon p {
  margin: 0;
}

.openBaloonBtn:focus + .balloon,
.balloon {
  visibility: visible;
  opacity: 1;
  transform: translateX(-50%) scale(1);
}

.taskOptions {
  position: relative;
  display: inline-block;
}
</style>