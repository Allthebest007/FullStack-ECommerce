import React from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { fetchProduct } from "../../api";
import {Button,Image,Text,Box} from "@chakra-ui/react";
import {useBasket} from '../../contexts/BasketContext'

function ProductDetail() {
  const { product_id } = useParams();
  const { isLoading, error, data } =  useQuery(["product",product_id],() => fetchProduct(product_id));
  // useEffect(() => {
  //   const data = axios.get(`http://localhost:46202/api/product/${product_id}`).then(res=>setinitialData(res.data.data))
  // }, []);
  const {addToBasket,items} = useBasket();

  
  //console.log(initialData);
  if(isLoading ){
    return <div>Loading...</div>
  }
  if(error){
    return <div>Error</div>
  }
  
  const findBasketItem = items.find((item) => item.id == product_id);

  return <div>
    
    <Text as="h2" fontSize="2xl">
      {data.name}
    </Text>
    <p>{data?.description}</p>
    <Box margin="10">
    <Image
          htmlWidth={400}
          htmlHeight={400} 
          src={`data:image/jpeg;base64,${data.productImage.image}`}
          alt="product"
          loading="lazy"
        ></Image>
    </Box>
    <Button colorScheme={findBasketItem ? "pink" : "green"} onClick={() => addToBasket(data,findBasketItem)}>
      {
        findBasketItem ? 'Remove from basket' : 'Add to basket'
      }
      </Button>
    </div>;
}

export default ProductDetail;
