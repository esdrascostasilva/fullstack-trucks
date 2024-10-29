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
  
  const API_URL_CAMINHAO = 'http://localhost:5262'

  useEffect(() => {
    const getCaminhao = async () => {
      try {
        setLoading(true);
        const response = await fetch(`${API_URL_CAMINHAO}/api/caminhao`)
        const dataApi = await response.json()

        setCaminhoes(dataApi)
      } catch (error) {
          console.error('Erro ao fazer ao fazer a requisicao Get na API de Caminhoes:', error)
          setError('Erro ao carregar dados')
      } finally {
        setLoading(false)
      }
    }
    getCaminhao()
  }, [])
  
  console.log('Estado atual dos caminhões:', caminhoes);
  


  
  
  return (
      <>
        <h1>This is my Home page</h1>
        
        <div className={styles.container}>
            <h1 className={styles.title}>Lista de Caminhões</h1>
            
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

