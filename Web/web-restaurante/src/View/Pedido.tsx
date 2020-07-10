import React from "react";
import { Segment, Dimmer, Loader, Form, Grid, Button, Modal, Table, Header } from "semantic-ui-react";
import { IDishModel } from "../Model/DishModel";
import dishesRespository, { IDishRepositoryResponse } from "../Repositories/DishesRepository";
import PedidoSelect from "./PedidoSelect";
import DropdownTimeDays from "../Component/DropdownTimeDays";

export interface IPedidoState{
    loading: boolean,
    dishesModel: IDishModel[],
    showPedidoSelect: boolean,
    showRemoveDishes: boolean,
    showModalResult: boolean,
    timeDayIdSelected: number,
    dishesModelSelected: IDishModel[]
}

export default class Pedido extends React.Component<{}, IPedidoState> {
    private responseDishes: IDishRepositoryResponse = { };

    constructor(props: any){
        super(props);

        this.state={
            loading: false,
            dishesModel: [],
            showPedidoSelect: false,
            showRemoveDishes: false,
            showModalResult: false,
            timeDayIdSelected: 0,
            dishesModelSelected: []
        }
    }

    public onChangeTimeDay = async (ev: any, element: any) => {
        this.setState({
            loading: true
        })

        let dishesModel: IDishModel[] = await dishesRespository.GetAllByTimeDayId(element.value);

        this.setState({
            ...this.state,
            loading: false,
            dishesModel: dishesModel,
            showPedidoSelect: true,
            timeDayIdSelected: element.value
        });
    }

    public showHiddenPedidoSelect = ( show: boolean ) =>{
        this.setState({ showPedidoSelect: show });
    }

    public setPedidosSelect = (dishesModel: IDishModel[]) =>{
        let auxDishesModel: IDishModel[] = JSON.parse(JSON.stringify((this.state.dishesModelSelected || [])));

        if( ( dishesModel || [] ).length > 0 ){

            auxDishesModel = auxDishesModel.concat( dishesModel );

            this.setState({
                dishesModelSelected: auxDishesModel
            });
        }
    }

    public onClose = () =>{
        if( ( this.state.dishesModel || [] ).length > 0 ){
            this.setState({
                showRemoveDishes: true
            });
        }
    }

    public onSave = async () =>{
        const dishesModelSelected = this.state.dishesModelSelected;

        if( ( dishesModelSelected || [] ).length > 0 ){

            this.responseDishes = await dishesRespository.PostOrder(
                this.state.timeDayIdSelected,
                dishesModelSelected?.map(m => m.dishTypeId)
            );

            this.setState({ showModalResult: true });
        }
    }

    public onCloseModalPedidos = () =>{
        this.setState({ showRemoveDishes: false });
    }

    public onCancelModalPedidos = () =>{
        this.setState({
            showRemoveDishes: false,
            dishesModel: []
        });
    }

    public onRemoveDish = (dishTypeId: number) =>{
        const dishModel: IDishModel[] = JSON.parse(JSON.stringify( this.state.dishesModelSelected || [] ));

        var idx = dishModel.findIndex( f => f.dishTypeId === dishTypeId );
        if( idx >= 0 ){

            dishModel.splice( idx, 1 );

            this.setState({
                dishesModelSelected: dishModel
            });
        }
    }

    public onCloseModalResult = () => {
        this.setState({
            showModalResult: false,
            dishesModelSelected: []
        });
    }

    public render(){
        const dishes = this.state.dishesModelSelected || [];

        return(
            <Segment>
                <Dimmer active={this.state.loading} inverted>
                    <Loader content="Loading"/>
                </Dimmer>
                <Form>
                    <Grid>
                        <Grid.Column width={7}>
                            <DropdownTimeDays
                                loading={this.state.loading}
                                onChangeTimeDay={this.onChangeTimeDay.bind( this )}
                            />
                        </Grid.Column>
                        {
                            this.state.timeDayIdSelected > 0
                            ?   <Grid.Column>
                                    <Button onClick={() => this.setState({ showPedidoSelect: true })} positive={true} icon='plus' />
                                </Grid.Column>
                            : null
                        }
                    </Grid>
                </Form>
                <Table color={dishes.length > 0 ? "green" : undefined} celled={true}>
                    { dishes.length > 0 ?
                        <Table.Header>
                            <Table.Row>
                                <Table.HeaderCell>Tipo de prato</Table.HeaderCell>
                                <Table.HeaderCell>Comida</Table.HeaderCell>
                                <Table.HeaderCell width="1">Remover</Table.HeaderCell>
                            </Table.Row>
                        </Table.Header>
                    : 
                    <Table.Header>
                        <Table.Row textAlign="center" warning={true}>
                            <Header as='h4'>Sem pedidos</Header>
                        </Table.Row>
                    </Table.Header>
                    }
                    <Table.Body>
                        { dishes.map( obj =>
                            <Table.Row key={obj.dishTypeId}>
                                <Table.Cell>{obj.dishType}</Table.Cell>
                                <Table.Cell>{obj.dish}</Table.Cell>
                                <Table.Cell>
                                    <Button icon='minus' negative={true} onClick={this.onRemoveDish.bind(this, obj.dishTypeId)} />
                                </Table.Cell>
                            </Table.Row>
                        ) }
                    </Table.Body>
                </Table>

                <PedidoSelect
                    show={this.state.showPedidoSelect}
                    dishesModel={this.state.dishesModel}
                    showHiddenPedidoSelect={this.showHiddenPedidoSelect}
                    setPedidosSelect={this.setPedidosSelect}
                />

                <Segment>
                    <Button.Group >
                        <Button onClick={this.onClose}>Cancelar</Button>
                        <Button.Or />
                        <Button onClick={this.onSave} positive={true}>Finalizar</Button>
                    </Button.Group>
                </Segment>
                <Modal key="modalRemoveAllPedidos" size="mini" open={this.state.showRemoveDishes}>
                    <Modal.Header>Cancelar pedido</Modal.Header>
                    <Modal.Content>
                        <p>Deseja remover todos os pedidos?</p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button onClick={this.onCloseModalPedidos} negative={true} icon='close' labelPosition='left' content='Não' />
                        <Button onClick={this.onCancelModalPedidos} positive={true} icon='checkmark' labelPosition='left' content='Sim' />
                    </Modal.Actions>
                </Modal>

                <Modal key="modalResultDishes" size="tiny" open={this.state.showModalResult}>
                    <Modal.Header>Resultado do pedido</Modal.Header>
                    <Modal.Content>
                            <p>{this.responseDishes.result}</p>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button onClick={this.onCloseModalResult} positive={true} icon='checkmark' labelPosition='left' content='Fechar' />
                    </Modal.Actions>
                </Modal>
                
            </Segment>
        );
    }
}