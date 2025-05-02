import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

interface TheHistory {
  randomNumber: number;
  history: {id: number, randomNumber: string}[]
}

function App() {
  const [count, setCount] = useState(0)
  const [random, setRandom] = useState(0);
  const [history, setHistory] = useState<TheHistory['history']>([]);

  const getData = async () => {
    const response = await fetch('api/data');
    const jsond: TheHistory = await response.json()

    setRandom(jsond.randomNumber)
    setHistory(jsond.history)
  }

  useEffect(() =>{
    getData()
  }, [count])


  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
        <p>
          <span>My server random number is: {random}</span>
        </p>
        <ul>
          {history.map(x => <li key={x.id}>{x.randomNumber}</li>)}
        </ul>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
