import {useRef,useState} from "react";
import { useBasket } from "../../contexts/BasketContext";
import { Link } from "react-router-dom";
import {
  Alert,
  Image,
  Button,
  Box,
  Text,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  FormControl,
  FormLabel,
  Input
} from "@chakra-ui/react";
import { postOrder } from "../../api";

function Basket() {
    const [name,setName] = useState("");
    const [surname,setSurname] = useState("");
    const [phoneNumber,setPhoneNumber] = useState("");
    const [deliveryAddress,setdeliveryAddress] = useState("");

    const handleSubmitForm = async() => {
        const itemIds = items.map((item) => item.id)

        const input = {
            "AddressInfo" : {
                name,
                surname,
                phoneNumber,
                deliveryAddress
            },
            "ItemsIds" : itemIds
        }
        const response = await postOrder(input)
        console.log(response);
        if(response.errors !== null){
          if(response.errors.errors.length > 0){
            var ul = document.getElementById("error");
            if(ul.hasChildNodes){
              ul.innerHTML ="";
            }
  
            response.errors.errors.forEach(item => {
              var li = document.createElement("li"); 
              li.innerHTML = item; 
              li.style.border = '1px solid red';
              ul.appendChild(li);
            });
            
  
          }
        }
        else{
  
          emptyBasket();
          onClose();
        }
        
        
    }

    const { isOpen, onOpen, onClose, } = useDisclosure()
    const initialRef = useRef()
  const { items, removeFromBasket,emptyBasket } = useBasket();
  const total = items.reduce((acc, obj) => acc + obj.price, 0);
  
  

  return (
    <Box p="5">
      {items.length < 1 && (
        <Alert status="warning"> You have not any item in your basket.</Alert>
      )}
      {items.length > 0 && (
        <>
          <ul style={{ listStyleType: "decimal" }}>
            {items.map((item) => (
              <li key={item.id} style={{ marginBottom: 15 }}>
                <Link to={`/product/${item.id}`}>
                  {item.name} - {item.price} TL
                  <Image
                    loading="lazy"
                    htmlWidth={200}
                    src={`data:image/jpeg;base64,${item.productImage.image}`}
                    alt="basket item"
                  />
                </Link>
                <Button
                  mt="2"
                  size="sm"
                  colorScheme="pink"
                  onClick={() => removeFromBasket(item.id)}
                >
                  Remove From Basket
                </Button>
              </li>
            ))}
          </ul>
          <Box mt="10">
            <Text fontSize="22">Total : {total} TL</Text>
          </Box>

          <Button mt="2" size="sm" colorScheme="green" onClick={onOpen}>
            Order
          </Button>

          <Modal
        initialFocusRef={initialRef}
        isOpen={isOpen}
        onClose={onClose}
      >
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Order</ModalHeader>
          <ModalCloseButton />
          <ModalBody pb={6}>
            <FormControl>
              <FormLabel>First Name</FormLabel>
              <Input ref={initialRef} placeholder='First name' onChange={(e) => setName(e.target.value)} />
            </FormControl>
            <FormControl mt={4}>
              <FormLabel>Last name</FormLabel>
              <Input placeholder='Last name' onChange={(e) => setSurname(e.target.value)}/>
            </FormControl>
            <FormControl mt={4}>
              <FormLabel>PhoneNumber</FormLabel>
              <Input placeholder='PhoneNumber' onChange={(e) => setPhoneNumber(e.target.value)}/>
            </FormControl>
            <FormControl mt={4}>
              <FormLabel>DeliveryAddress</FormLabel>
              <Input placeholder='DeliveryAddress' onChange={(e) => setdeliveryAddress(e.target.value)} />
            </FormControl>
            <ul id="error"></ul>
            
          </ModalBody>

          <ModalFooter>
            <Button colorScheme='blue' mr={3} onClick={handleSubmitForm}>
              Save
            </Button>
            <Button onClick={onClose}>Cancel</Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
        </>
      )}
    </Box>
  );
}

export default Basket;
