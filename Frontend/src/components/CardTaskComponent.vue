<template>
  <div class="taskList">
    <div 
      v-for="(task, index) in tasks" 
      :key="index" 
      class="taskCard">
      <div class="cardHeader">
        <h3 class="taskTitle">{{ task.title }}</h3>
        <div>
          <span class="taskStatus">{{ task.status }}</span>
          <button class="expandBtn" @click="toggleExpand(index)">
            {{ expandedIndex === index ? '-' : '+' }}
          </button>
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

const toggleExpand = (index) => {
  expandedIndex.value = expandedIndex.value === index ? null : index;
};
</script>

<style>
.taskList {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 16px;
}
  
.taskCard {
  display: flex;
  flex-direction: column;
  background-color: #fff;
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
</style>