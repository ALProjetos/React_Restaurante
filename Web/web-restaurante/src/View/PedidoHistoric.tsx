import React from "react";
import { Segment, Dimmer, Loader, Table, Header, Form, Grid } from "semantic-ui-react";
import { IDishHistoricModel } from "../Model/DishHistoricModel";
import dishesRespository from "../Repositories/DishesRepository";
import DropdownTimeDays from "../Component/DropdownTimeDays";

export interface IPedidoHistoricState{
    loading: boolean,
    dishesHistoric: IDishHistoricModel[]
}

export default class PedidoHistoric extends React.Component<{}, IPedidoHistoricState> {
    constructor(props: any){
        super(props);

        this.state = {
            loading: false,
            dishesHistoric: []
        }
    }

    public onChangeTimeDay = async (ev: any, element: any) => {
        this.setState({
            loading: true
        })

        var dishesHistoric = await dishesRespository.GetHistoricTimeDayId(element.value);

        this.setState({
            ...this.state,
            loading: false,
            dishesHistoric: dishesHistoric
        });
    }

    public FormatDate( date: Date ): string {

        let options: any = {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
            hour: "numeric",
            minute: "numeric",
            second: "numeric"
        };

        return new Intl.DateTimeFormat( window.navigator.language, options ).format( new Date( date ) );
    }

    public render(){
        const dishesHistoric = ( this.state.dishesHistoric || [] );

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
                    </Grid>
                </Form>
                <Table color={dishesHistoric.length > 0 ? "green" : undefined} celled={true} columns="3">
                    { dishesHistoric.length > 0 ?
                        <Table.Header>
                            <Table.Row>
                                <Table.HeaderCell>Data</Table.HeaderCell>
                                <Table.HeaderCell>Tipo de prato</Table.HeaderCell>
                                <Table.HeaderCell>Comida</Table.HeaderCell>
                            </Table.Row>
                        </Table.Header>
                    : 
                    <Table.Header>
                        <Table.Row textAlign="center" warning={true}>
                            <Header as='h4'>Sem histórico</Header>
                        </Table.Row>
                    </Table.Header>
                    }
                    <Table.Body>
                        { dishesHistoric.map( obj =>
                            (obj.dishes || []).map( dish =>
                                <Table.Row key={dish.dishTypeId}>
                                    <Table.Cell>{this.FormatDate(obj.date)}
                                    </Table.Cell>
                                    <Table.Cell>{dish.dishType}</Table.Cell>
                                    <Table.Cell>{dish.dish}</Table.Cell>
                                </Table.Row>
                            )
                        ) }
                    </Table.Body>
                </Table>
            </Segment>
        );
    }
}