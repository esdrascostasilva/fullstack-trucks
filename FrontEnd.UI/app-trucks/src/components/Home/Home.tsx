import React, { useEffect, useState } from 'react'
import styles from './Home.module.css'


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
  const [editando, setEditando] = useState<CaminhaoData | null>(null)
  const [novoRegistro, setNovoRegistro] = useState<Omit<CaminhaoData, 'id'> | null>(null)
  const [modelos, setModelos] = useState<Modelo[]>([])
  const [plantas, setPlantas] = useState<Planta[]>([])

  const URL_BASE = 'http://localhost:5262'
  const URL_PATH = '/api/caminhao'

  useEffect(() => {
    getCaminhoes()
    getModelos()
    getPlantas()
  }, [])

  const getCaminhoes = async () => {
    try {
      setLoading(true)
      const response = await fetch(`${URL_BASE}${URL_PATH}`)
      const dataApi = await response.json()

      setCaminhoes(dataApi)
    } catch (error) {
      console.error('Erro ao fazer ao fazer a requisicao Get na API de Caminhoes:', error)
      setError('Erro ao carregar dados')
    } finally {
      setLoading(false)
    }
  }

  const handleNewTruck = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!novoRegistro)
      return

    const novoCaminhao = {
      anoFabricacao: novoRegistro.anoFabricacao,
      codigoChassi: novoRegistro.codigoChassi,
      cor: novoRegistro.cor,
      modeloId: novoRegistro.modeloId,
      plantaId: novoRegistro.plantaId
    }

    try {
      const response = await fetch(`${URL_BASE}${URL_PATH}`, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(novoCaminhao)
      })

      if (!response.ok)
        throw new Error('Erro ao criar um novo caminhao')

      await getCaminhoes()
      setNovoRegistro(null)

    } catch (error) {
      console.error('Erro na criação:', error)
      alert('Ocorreu um erro ao criar um novo caminhao')
    }
  }

  const handleUpdate = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!editando)
      return

    try {
      const response = await fetch(`${URL_BASE}${URL_PATH}/${editando.id}`, {
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

  const handleNovoRegistroChange = (campo: keyof Omit<CaminhaoData, 'id'>, valor: any) => {
    if (!novoRegistro) return

    setNovoRegistro({
      ...novoRegistro,
      [campo]: valor
    })
  }

  const handleDelete = async (id: number) => {
    if (!window.confirm('Excluir Caminhao'))
      return

    try {
      const response = await fetch(`${URL_BASE}${URL_PATH}/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok)
        throw new Error('Erro ao excluir o caminhão')

      await getCaminhoes()

    } catch (error) {
      console.error('Erro ao excluir:', error)
      alert('Ocorreu um erro ao excluir o caminhão')
    }
  }

  const getModelos = async () => {
    try {
      const response = await fetch(`${URL_BASE}/api/modelo`)
      const data = await response.json()
      setModelos(data)
    } catch (error) {
      console.error('Erro ao buscar modelos:', error)
    }
  }

  const getPlantas = async () => {
    try {
      const response = await fetch(`${URL_BASE}/api/planta`)
      const data = await response.json()
      setPlantas(data)
    } catch (error) {
      console.error('Erro ao buscar plantas:', error)
    }
  }

  if (loading) return <div>Carregando...</div>
  if (error) return <div>Erro: {error}</div>


  return (
    <>

      <div className={styles.container}>
        <h1 className={styles.title}>Sistema de Cadastro de Caminhões</h1>

        <h1 className={styles.title}>Lista de Caminhões</h1>

        {/* Botão para criar um novo caminhao */}
        {!editando && !novoRegistro && (
          <button
            onClick={() => setNovoRegistro({
              anoFabricacao: 0,
              codigoChassi: '',
              cor: '',
              modeloId: 0,
              plantaId: 0,
              modelo: { id: 0, nome: '' },
              planta: { id: 0, nome: '' }
            })}
            className={styles.newButton}
          >
            Novo Caminhão
          </button>
        )}

        {/* Formulário (serve tanto para edição quanto para criação) */}
        {(editando || novoRegistro) && (
          <div className={styles.editForm}>
            <h3>{editando ? `Editando Caminhão ID: ${editando.id}` : 'Novo Caminhão'}</h3>
            <form onSubmit={editando ? handleUpdate : handleNewTruck}>

              <div className={styles.formGroup}>
                <label>Modelo:</label>
                <select
                  value={editando ? editando.modeloId : (novoRegistro?.modeloId || '')}
                  onChange={(e) => {
                    const selectedId = parseInt(e.target.value)
                    const selectedModelo = modelos.find(m => m.id === selectedId)

                    if (editando) {
                      setEditando({
                        ...editando,
                        modeloId: selectedId,
                        modelo: selectedModelo!
                      })
                    } else if (novoRegistro && selectedModelo) {
                      setNovoRegistro({
                        ...novoRegistro,
                        modeloId: selectedId,
                        modelo: selectedModelo
                      })
                    }
                  }}
                >
                  <option value="">Selecione um modelo</option>
                  {modelos.map(modelo => (
                    <option key={modelo.id} value={modelo.id}>
                      {modelo.nome}
                    </option>
                  ))}
                </select>
              </div>

              <div className={styles.formGroup}>
                <label>Planta:</label>
                <select
                  value={editando ? editando.plantaId : (novoRegistro?.plantaId || '')}
                  onChange={(e) => {
                    const selectedId = parseInt(e.target.value)
                    const selectedPlanta = plantas.find(p => p.id === selectedId)

                    if (editando) {
                      setEditando({
                        ...editando,
                        plantaId: selectedId,
                        planta: selectedPlanta!
                      })
                    } else if (novoRegistro && selectedPlanta) {
                      setNovoRegistro({
                        ...novoRegistro,
                        plantaId: selectedId,
                        planta: selectedPlanta
                      })
                    }
                  }}
                >
                  <option value="">Selecione uma planta</option>
                  {plantas.map(planta => (
                    <option key={planta.id} value={planta.id}>
                      {planta.nome}
                    </option>
                  ))}
                </select>
              </div>

              <div className={styles.formGroup}>
                <label>Chassi:</label>
                <input
                  type="text"
                  value={editando ? editando.codigoChassi : (novoRegistro?.codigoChassi || '')}
                  onChange={(e) => {
                    if (editando) {
                      setEditando({ ...editando, codigoChassi: e.target.value })
                    } else {
                      handleNovoRegistroChange('codigoChassi', e.target.value)
                    }
                  }}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Cor:</label>
                <input
                  type="text"
                  value={editando ? editando.cor : (novoRegistro?.cor || '')}
                  onChange={(e) => {
                    if (editando) {
                      setEditando({ ...editando, cor: e.target.value })
                    } else {
                      handleNovoRegistroChange('cor', e.target.value)
                    }
                  }}
                />
              </div>

              <div className={styles.formGroup}>
                <label>Ano de Fabricação:</label>
                <input
                  type="number"
                  value={editando ? editando.anoFabricacao : (novoRegistro?.anoFabricacao || '')}
                  onChange={(e) => {
                    if (editando) {
                      setEditando({ ...editando, anoFabricacao: Number(e.target.value) })
                    } else {
                      handleNovoRegistroChange('anoFabricacao', Number(e.target.value))
                    }
                  }}
                />
              </div>

              <div className={styles.formButtons}>
                <button type="submit">
                  {editando ? 'Salvar' : 'Criar'}
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setEditando(null)
                    setNovoRegistro(null)
                  }}
                >
                  Cancelar
                </button>
              </div>
            </form>
          </div>
        )}


        {/* Get */}
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
                  <button
                    onClick={() => handleDelete(caminhao.id)}
                    className={styles.deleteButton}
                  >
                    Deletar
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
  )
}

export default Home

function setLoading(arg0: boolean) {
  throw new Error('Function not implemented.')
}
function setError(arg0: string) {
  throw new Error('Function not implemented.')
}

