import React from "react";
import { Segment, Card } from "semantic-ui-react";
import Cardapio from "./View/Cardapio";
import Pedido from "./View/Pedido";
import PedidoHistoric from "./View/PedidoHistoric";

export interface IAppState{
    optionMenu: number
}

export class App extends React.Component<{}, IAppState> {
    constructor(props: any){
      super(props);

      this.state={
          optionMenu: 0
      }
    }

    public render(){

        return(
            <Segment color="blue">
                <Card.Group itemsPerRow={4}>
                    <Card
                        link
                        color={this.state.optionMenu === 1 ? "green" : undefined}
                        onClick={() => this.setState({ optionMenu: 1 })}
                        header="Cardápio"
                    />
                    <Card
                        link
                        color={this.state.optionMenu === 2 ? "green" : undefined}
                        onClick={() => this.setState({ optionMenu: 2 })}
                        header="Pedido"
                    />
                    <Card
                        link
                        color={this.state.optionMenu === 3 ? "green" : undefined}
                        onClick={() => this.setState({ optionMenu: 3 })}
                        header="Histórico"
                    />
                </Card.Group>
                {
                    this.state.optionMenu === 1
                    ? <Cardapio />
                    : null
                }
                {
                    this.state.optionMenu === 2
                    ? <Pedido />
                    : null
                }
                {
                    this.state.optionMenu === 3
                    ? <PedidoHistoric />
                    : null
                }
            </Segment>
        )
    }
}

export default App;