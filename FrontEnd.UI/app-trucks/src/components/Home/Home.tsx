import React, { useEffect, useState } from 'react'
import axios from 'axios'
import styles from './Home.module.css';

interface CaminhaoData {
  id: number,
  anoFabricacao: number
  codigoChassi: string,
  cor: string,
  modeloId: number,
  plantaId: number,
  modelo: Modelo,
  planta: Planta,
}

interface Modelo {
  id: number
  nome: string
}

interface Planta {
  id: number
  nome: string
}

function Home() {

  const [caminhoes, setCaminhoes] = useState<CaminhaoData[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [editando, setEditando] = useState<CaminhaoData | null>(null);

  const URL_BASE = 'http://localhost:5262'

  useEffect(() => {
    getCaminhoes()
  }, [])

  const getCaminhoes = async () => {
    try {
      setLoading(true);
      const response = await fetch(`${URL_BASE}/api/caminhao`)
      const dataApi = await response.json()

      setCaminhoes(dataApi)
    } catch (error) {
      console.error('Erro ao fazer ao fazer a requisicao Get na API de Caminhoes:', error)
      setError('Erro ao carregar dados')
    } finally {
      setLoading(false)
    }
  }

  const handleUpdate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!editando)
      return

    try {
      const response = await fetch(`${URL_BASE}/api/caminhao/${editando.id}`, {
        method: 'PUT',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(editando)
      })

      if (!response.ok)
        throw new Error('Erro ao atualizar os dados')

      await getCaminhoes()
      setEditando(null)

    } catch (error) {
      console.error('Erro na edição: ', error)
      alert('Ocorreu um erro ao atualizar o caminhao')
    }
  }


  if (loading) return <div>Carregando...</div>;
  if (error) return <div>Erro: {error}</div>;


  return (
    <>
      <h1>This is my Home page</h1>

      <div className={styles.container}>
        <h1 className={styles.title}>Lista de Caminhões</h1>

        {/* Formulário de Edição */}
        {editando && (
          <div className={styles.editForm}>
            <h3>Editando Caminhão ID: {editando.id}</h3>
            <form onSubmit={handleUpdate}>
              <div className={styles.formGroup}>
                <label>Chassi:</label>
                <input
                  type="text"
                  value={editando.codigoChassi}
                  onChange={(e) => setEditando({ ...editando, codigoChassi: e.target.value })}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Cor:</label>
                <input
                  type="text"
                  value={editando.cor}
                  onChange={(e) => setEditando({ ...editando, cor: e.target.value })}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Ano de Fabricação:</label>
                <input
                  type="number"
                  value={editando.anoFabricacao}
                  onChange={(e) => setEditando({ ...editando, anoFabricacao: Number(e.target.value) })}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Modelo:</label>
                <input
                  type="number"
                  value={editando.modeloId}
                  onChange={(e) => setEditando({ ...editando, modeloId: Number(e.target.value) })}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Planta:</label>
                <input
                  type="number"
                  value={editando.plantaId}
                  onChange={(e) => setEditando({ ...editando, plantaId: Number(e.target.value) })}
                />
              </div>

              <div className={styles.formButtons}>
                <button type="submit">Salvar</button>
                <button type="button" onClick={() => setEditando(null)}>Cancelar</button>
              </div>
            </form>
          </div>
        )}



        <table className={styles.table}>
          <thead className={styles.tableHeader}>
            <tr>
              <th>Modelo</th>
              <th>Chassi</th>
              <th>Cor</th>
              <th>Ano de Fabricação</th>
              <th>Planta</th>
            </tr>
          </thead>
          <tbody>
            {caminhoes.map(caminhao => (
              <tr key={caminhao.id} className={styles.tableRow}>
                <td>{caminhao.modelo.nome}</td>
                <td>{caminhao.codigoChassi}</td>
                <td>{caminhao.cor}</td>
                <td>{caminhao.anoFabricacao}</td>
                <td>{caminhao.planta.nome}</td>
                <td>
                  <button
                    onClick={() => setEditando(caminhao)}
                    className={styles.editButton}
                  >
                    Editar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>






        {caminhoes.length === 0 && (
          <p className={styles.emptyMessage}>
            Nenhum caminhão encontrado.
          </p>
        )}
      </div>
    </>
  );
}

export default Home;

function setLoading(arg0: boolean) {
  throw new Error('Function not implemented.');
}
function setError(arg0: string) {
  throw new Error('Function not implemented.');
}

