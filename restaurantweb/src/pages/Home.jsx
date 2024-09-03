import logo from '../../public/the-cozy-plate-logo.svg';
import interior from '../../public/restaurant-interior-01.webp';
import './Home.css'

function Home() {
  return (
      <div>
          <img src={logo} alt="The Cozy Plate Logo" className="logo" />
          <img src={interior} alt="The Cozy Plate interior" className="interior-image" />
      </div>
  );
}

export default Home;