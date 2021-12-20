import React from "react";
import Card from "../../components/Card";
import { Grid } from "@chakra-ui/react";
import { useQuery } from "react-query";
import { fetchProductList } from "../../api";
import Category from "../../components/Category";
import Slider from "../../components/Slider/Slider";
function Products() {
  const { isLoading, error, data } = useQuery("products", fetchProductList);

  if (isLoading) return "Loading...";

  if (error) return "An error has occurred: " + error.message;

  return (
    <div>
      <Slider />
      <div style={{ display: "flex" }}>
        <Category />
        
        <div style={{ flex: "1" }}>
          <Grid templateColumns="repeat(3,1fr)" gap={4}>
            {data.map((item, key) => (
              <Card key={key} item={item}></Card>
            ))}
          </Grid>
        </div>
      </div>
    </div>
  );
}

export default Products;
