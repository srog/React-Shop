class CheckoutConfirmView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {};

        this.state = {
            IsPaymentSelected: false,
            IsAddressSelected: false,
            DeliveryAddressId: this.props.model.DeliveryAddressId,
            PaymentOptionId: this.props.model.PaymentOptionId
        };
    }

     UpdateDeliveryAddressId(event) {
         if (event.target.value === this.state.DeliveryAddressId) {
             this.setState({
                 DeliveryAddressId: 0,
                 IsAddressSelected: false
             });
             event.target.checked = false;

         } else {
             this.setState({
                 DeliveryAddressId: event.target.value,
                 IsAddressSelected: true
             });
             event.target.checked = true;
         }
     }


     UpdatePaymentOptionId(event) {
         if (event.target.value === this.state.PaymentOptionId) {
             this.setState({
                 PaymentOptionId: 0,
                 IsPaymentSelected: false
             });
             event.target.checked = false;

         } else {
             this.setState({
                 PaymentOptionId: event.target.value,
                 IsPaymentSelected: true
             });
             event.target.checked = true;
         }
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

                <br />

                    <Row>
                        <h3>
                            Please select a delivery address
                        </h3>
                     </Row>

                <Panel>
                    <Row>
                        <Column md={6}>
                            <h4>Address</h4>
                        </Column>
                        <Column md={6}>
                            <h5 className="float-right">Selected</h5>
                        </Column>
                    </Row>

                    {
                        this.props.model.CustomerInfo.Addresses.map(address =>
                        <Row className="gray">
                            <Column md={6}>
                                <div className="offset30 truncate">
                                {address.DisplayAddress}
                                </div>
                            </Column>

                            <Column md={6} className="pull-right">
                            <p>
                                <div className="float-right" onChange={this.UpdateDeliveryAddressId.bind(this)}>
                                    <input name="addressRadio"
                                           type="radio" 
                                           value={address.Id} 
                                            />
                                </div>
                            </p>
                            </Column>
                        </Row>
                        )
                    }

                </Panel>

                <Row>
                        <h3>
                            Please select a payment option
                        </h3>
                </Row>

                <Panel>
                    <Row>
                        <Column md={6}>
                            <h4>PaymentOption</h4>
                        </Column>
                        <Column md={6}>
                            <h5 className="float-right">Selected</h5>
                        </Column>
                    </Row>

                    {
                    this.props.model.CustomerInfo.PaymentOptions.map(paymentoption =>
                        <Row className="gray">
                            <Column md={6}>
                                <div className="offset30 truncate">{paymentoption.PaypalEmail}
                                </div>
                            </Column>

                            <Column md={6} className="pull-right">
                            <p>
                                <div className="float-right" onChange={this.UpdatePaymentOptionId.bind(this)}>
                                    <input name="paymentRadio"
                                           type="radio"
                                           value={paymentoption.Id} />
                                </div>
                            </p>
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
                        
                        {this.state.IsPaymentSelected === true && this.state.IsAddressSelected === true ?
                        <a href={this.props.urlCheckoutSuccess}>
                            <button type="button" className="btn btn-success pull-right">Confirm and Place Order</button>
                        </a>
                        : null
                    }

                    </Column>
                </Row>
        </div>

            );
    }
}

CheckoutConfirmView.propTypes = {

};