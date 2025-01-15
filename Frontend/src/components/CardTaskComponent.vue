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
          <button class="openBaloonBtn" @click="toggleBalloon(index)">
            <font-awesome-icon :icon="['fas', 'ellipsis-vertical']" />
          </button>
        
          <div v-if="isBalloonOpen[index]" class="balloon">
            <font-awesome-icon :icon="['fas', 'pen-to-square']" />
            <font-awesome-icon 
              :icon="['fas', 'trash']" 
              @click="openDialog" 
            />
          </div>
      
          <!-- Dialog de exclusão -->
          <div v-if="isDialogOpen" class="dialog-overlay" @click.self="closeDialog">
            <div class="dialog-content">
              <h3 class="titleDialog">Excluir Tarefa</h3>
              <p>Deseja excluir esta tarefa?</p>
      
              <div>
                <button @click="closeDialog">Cancelar</button>
                <button @click="confirmDelete">Excluir</button>
              </div>
            </div>
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
const isBalloonOpen = ref([]);
const isDialogOpen = ref(false); 
const dialogType = ref(''); 

const openDialog = () => {
  dialogType.value = 'delete';
  isDialogOpen.value = true; 
};

const closeDialog = () => {
  isDialogOpen.value = false; 
};

const confirmDelete = () => {
  closeDialog();
};

const toggleBalloon = (index) => {
  isBalloonOpen.value[index] = !isBalloonOpen.value[index];
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

.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

/* Estilos para o conteúdo do dialog */
.dialog-content {
  display: flex;
  flex-direction: column; 
  align-items: center; 
  background-color: white;
  padding: 20px;
  border-radius: 20px;
  width: 300px;
  height: 200px;
  text-align: center;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  transform: scale(0.8);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

/* Animação de transição para o dialog */
.dialog-fade-enter-active,
.dialog-fade-leave-active {
  transition: opacity 0.3s ease;
}

.dialog-fade-enter, 
.dialog-fade-leave-to /* .dialog-fade-leave-active em versões anteriores do Vue */ {
  opacity: 0;
}

/* Efeito de relevo na transição do dialog */
.dialog-fade-enter-to {
  transform: scale(1);
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.2);
}

.titleDialog {
  padding-bottom: 25px;
}

.taskOptions {
  position: relative;
  display: inline-block;
}
</style>