import * as React from 'react';
import { Button, Label, Modal, Table } from 'semantic-ui-react';
import { IDishModel } from '../Model/DishModel';

 export interface IPedidoSelectState{
    dishesModel: IDishModel[],
    dishesModelSelected: IDishModel[]
 }

export interface IPedidoSelectProps{
    dishesModel: IDishModel[],
    show: boolean,
    showHiddenPedidoSelect: (show: boolean) => void,
    setPedidosSelect: (dishesModel: IDishModel[]) => void
}

export default class PedidoSelect extends React.Component<IPedidoSelectProps, IPedidoSelectState> {
    constructor(props: any){
      super(props);

      this.state ={
          dishesModel: this.props.dishesModel,
          dishesModelSelected: []
      }
    }

    public onAddItem = (dish: IDishModel) =>{
        const auxDishesModelSelected: IDishModel[] = JSON.parse(JSON.stringify(this.state.dishesModelSelected || []));

        if( null != dish ){
            auxDishesModelSelected.push(dish);

            this.setState({
                ...this.state,
                dishesModelSelected: auxDishesModelSelected
            });
        }
    }

    public onRemoveItem = (dish: IDishModel) =>{
        const auxDishesModelSelected: IDishModel[] = JSON.parse(JSON.stringify(this.state.dishesModelSelected || []));

        if( null != dish ){
            var idxSearch = auxDishesModelSelected.findIndex(f => f.dishTypeId === dish.dishTypeId);
            auxDishesModelSelected.splice( idxSearch, 1 );

            this.setState({
                ...this.state,
                dishesModelSelected: auxDishesModelSelected
            });
        }
    }

    public onClickCancel = () => {
        this.props.showHiddenPedidoSelect(!this.props.show);
    }

    public onClickSave = () =>{
        const dishesModelSelected = this.state.dishesModelSelected;

        if( ( dishesModelSelected || [] ).length > 0 ){
            this.props.setPedidosSelect( dishesModelSelected );
        }

        this.props.showHiddenPedidoSelect( !this.props.show );

        this.setState({
            dishesModelSelected: []
        });
    }

    public onGetQuantidade = (dishTypeId: number) =>{
        const auxDishesModelSelected: IDishModel[] = this.state.dishesModelSelected;

        return (auxDishesModelSelected.filter(f => f.dishTypeId === dishTypeId) || []).length;
    }

    public render() {

        if(this.state.dishesModel !== this.props.dishesModel){
            this.setState({dishesModel: this.props.dishesModel});
        }
    
        return (
            <Modal closeOnDimmerClick={false} size="small" open={this.props.show} onClose={this.onClickCancel}>
                <Modal.Header>Marque as comidas desejadas</Modal.Header>
                <Modal.Content scrolling={true}>
                <Table color="green" celled={true} columns={3}>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell>Tipo de prato</Table.HeaderCell>
                            <Table.HeaderCell>Comida</Table.HeaderCell>
                            <Table.HeaderCell>Quantidade</Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        { (this.props.dishesModel || []).map( obj =>
                            <Table.Row key={obj.dishTypeId}>
                                <Table.Cell >{obj.dishType}</Table.Cell>
                                <Table.Cell >{obj.dish}</Table.Cell>
                                <Table.Cell width="5">
                                    <Button icon='minus' onClick={this.onRemoveItem.bind(this, obj)} />
                                    <Label>{this.onGetQuantidade(obj.dishTypeId)}</Label>
                                    <Button icon='plus' onClick={this.onAddItem.bind(this, obj)} />
                                </Table.Cell>
                            </Table.Row>
                        ) }
                    </Table.Body>
                </Table>
                </Modal.Content>
                <Modal.Actions>
                    <Button onClick={this.onClickCancel} negative={true} icon='close' labelPosition='left' content='Cancelar' />
                    <Button onClick={this.onClickSave} positive={true} icon='checkmark' labelPosition='left' content='Salvar' />
                </Modal.Actions>
            </Modal>
        )
    }
}