import api from './api';

// Função para obter todas as tarefas
export const getTasks = async () => {
    try {
      const response = await api.get('/GetTasks');
      console.log('Resposta da API:', response.data); // Loga os dados recebidos
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar tarefas:', error);
      throw error;
    }
  };

// Função para obter uma tarefa pelo ID
export const getTaskById = async (idTask) => {
  try {
    const response = await api.get(`/GetTaskById/${idTask}`);
    return response.data;
  } catch (error) {
    console.error(`Erro ao buscar tarefa com ID ${idTask}:`, error);
    throw error;
  }
};



