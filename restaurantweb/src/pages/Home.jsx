import logo from '/the-cozy-plate-logo.svg';
import interior from '/restaurant-interior-01.webp';
import './Home.css'
import { Link } from 'react-router-dom'

function Home() {
  return (
      <div>
          <img src={logo} alt="The Cozy Plate Logo" className="logo" />
          <Link to="/menu"><img src={interior} alt="The Cozy Plate interior" className="interior-image" /></Link>
      </div>
  );
}

export default Home;