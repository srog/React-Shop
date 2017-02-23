class CheckoutConfirmView extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <br />
                <Row>
                    <span className="fa fa-check-circle"></span>
                </Row>
                <h3 className="text-center">
                    <span>Please confirm your order and details. Thank you!</span>
                </h3>
               
                <br />
                <h3>
                    Order Details:
                </h3>

                <Panel>
                    <Row>
                        <Column md={6}>
                          
                        </Column>
                        <Column md={6}>
                            <h4>Payment</h4>
                            <div className="boleto">
                                <p><i className="fa fa-paypal leading-icon" aria-hidden="true"></i> Total Price</p>
                                <p className="offset60"><Dollars val={this.props.model.Total} /></p>
                            </div>
                        </Column>
                    </Row>
                    

                    {
                        this.props.model.CartItems.map(item =>
                        <Row className="gray">
                            <Column md={6}>
                                <div className="offset30 truncate">
                                    <span>•</span>
                                    <span>{item.Description}</span>
                                </div>
                            </Column>
                            <Column md={6} className="pull-right">
                                <p className="float-right">{item.Quantity}</p>
                            </Column>
                        </Row>
                        )
                    }

                </Panel>
                <Row>
                    <Column md={3}>
                        <a href={this.props.urlCart}>
                            <button type="button" className="btn btn-success">Return to Cart Details</button>
                        </a>
                    </Column>
                    <Column md={3} className="pull-right">
                        <a href={this.props.urlCheckoutSuccess}>
                            <button type="button" className="btn btn-success pull-right">Confirm and Place Order</button>
                        </a>
                    </Column>
                </Row>

              
        </div>

            );
    }
}

CheckoutConfirmView.propTypes = {

};