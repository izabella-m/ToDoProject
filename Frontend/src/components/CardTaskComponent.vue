<template>
  <div class="taskList">
    <div 
      v-for="(task, index) in tasks" 
      :key="index" 
      class="taskCard">
      <div class="cardHeader">
        <h3 class="taskTitle">{{ task.title }}</h3>
        <div class="taskOptions">
          <!-- <span class="cardStatus">
            <p class="taskStatus">{{ task.status }}</p>
          </span> -->
          <button class="expandBtn" @click="toggleExpand(index)">
            <font-awesome-icon class="chevronStyle" :icon="expandedIndex === index ? ['fas', 'chevron-up'] : ['fas', 'chevron-down']" />
          </button>
          <button class="openBaloonBtn" @click="toggleBalloon(index)">
            <font-awesome-icon class="chevronStyle" :icon="['fas', 'ellipsis-vertical']" />
          </button>
        
          <div v-if="isBalloonOpen[index]" class="balloon">
            <font-awesome-icon class="iconEdit" :icon="['fas', 'pen-to-square']" @click="openEditDialog"/>
            <font-awesome-icon 
              class="iconTrash"
              :icon="['fas', 'trash']" 
              @click="openDeleteDialog" 
            />
          </div>
      
          <!-- Dialog de exclusão -->
          <div v-if="isDialogOpen" class="dialog-overlay" @click.self="closeDeleteDialog">
            <div class="dialogDeleteTask">
              <h3 class="titleDialogDeleteTask">Excluir Tarefa</h3>
              <p class="textConfirmeDelete">Deseja excluir esta tarefa?</p>
      
              <div>
                <button class="buttonCancelDelete" @click="closeDeleteDialog">Cancelar</button>
                <button class="buttonConfirmDelete" @click="confirmDelete">Excluir</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div v-if="expandedIndex === index" class="cardDescription">
        <p class="idTaskInDescription">Tarefa {{ task.id }}</p>
        <p class="textDescription">{{ task.description }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { defineEmits } from 'vue';


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
const emit = defineEmits(['edit-action']);

const openEditDialog = () => {
  emit('edit-action'); // Emite o evento para o pai
};

const openDeleteDialog = () => {
  dialogType.value = 'delete';
  isDialogOpen.value = true; 
};

const closeDeleteDialog = () => {
  isDialogOpen.value = false; 
};

const confirmDelete = () => {
  closeDeleteDialog();
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
  margin-top: 30px;
}
  
.taskCard {
  display: flex;
  flex-direction: column;
  background-color: #ffffff;
  border-radius: 16px;
  border: 1px solid #f2f2f2;
  padding: 15px;
  align-content: center;
}
  
.cardHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.taskTitle {
  font-size: 14px;
  font-weight: 600;
  margin: 0;
}

.taskStatus {
  font-size: 14px;
  font-weight: 500;
  color: #555;
}

.taskOptions {
  position: relative;
  display: inline-block;
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

.chevronStyle {
  color: #636e71;
  margin-left: 4px;
  margin-right: 4px;
  font-size: 13px;
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

.titleDialogDeleteTask {
  color: #832b2b;
  padding-bottom: 0px;
  font-size: 20px;
}

.textConfirmeDelete {
  padding: 0%;
  font-size: 16px;
  font-weight: 400;
}

.buttonCancelDelete {
  width: 90%;
  height: 40px;
  padding: 10px;
  margin-top: 10px;
  background-color: #01b894;
  color: white;
  border: none;
  border-radius: 14px;
  cursor: pointer;
  font-size: 16px;
  font-weight: 500;
}

.buttonConfirmDelete {
  background: none;
  border: none;
  color: inherit; 
  font: inherit;
  padding: 0; 
  margin-bottom: 15px;
  cursor: pointer; 
  color: #ccc;
}

.openBaloonBtn {
  margin-left: 4px;
  margin-right: 4px;
  font-size: 13px;
  position: relative; 
  padding: 0;
  border: none;
  font-size: 16px;
  cursor: pointer;
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

.iconEdit {
  color: #007bff;
  margin-bottom: 3px;
}

.iconTrash {
  color: #832b2b;
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
.dialogDeleteTask {
  display: flex;
  flex-direction: column; 
  align-items: center; 
  background-color: white;
  padding: 10px;
  border-radius: 20px;
  width: 250px;
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



.idTaskInDescription {
  font-size: 10px;
  color: #ccc;
}

.textDescription {
  color: #636E71;
  font-size: 12px;  
}
</style>