import api from './api';

export const getTasks = async () => {
    try {
      const response = await api.get('/GetTasks');
      console.log('Resposta da API:', response.data);
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar tarefas:', error);
      throw error;
    }
  };

export const getTaskById = async (idTask) => {
  try {
    const response = await api.get(`/GetTaskById/${idTask}`);
    return response.data;
  } catch (error) {
    console.error(`Erro ao buscar tarefa com ID ${idTask}:`, error);
    throw error;
  }
};

export const createTask = async (newTask) => {
  try {
    // Envia os dados da tarefa no corpo da requisição POST
    const response = await api.post('/CreateTask', newTask);
    console.log('Tarefa criada com sucesso:', response.data);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar tarefa:', error);
    throw error;
  }
};

export const deleteTask = async (idTask) => {
  try {
    const response = await api.delete('/DeleteTask', { params: { idTask } });
    console.log('Tarefa excluída com sucesso:', response.data);
    return response.data;  // Retorna os dados da resposta após excluir a tarefa
  } catch (error) {
    console.error(`Erro ao excluir tarefa com ID ${idTask}:`, error);
    throw error;  // Relança o erro para ser tratado no componente
  }
};





