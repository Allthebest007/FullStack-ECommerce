import { Box, Button } from '@chakra-ui/react';
import React, { useEffect } from 'react'
import {useQuery} from 'react-query'
import { Link } from 'react-router-dom';
import { fetchCategoryList } from '../../api';

function Category() {

    const { isLoading, error, data } = useQuery("categories",fetchCategoryList);

  if (isLoading) return "Loading...";

  if (error) return "An error has occurred: " + error.message;

    console.log(data);
    return (
        <div>
            {data.map((item,key) => <Box mt="5" textColor="black" fontWeight="900" key={item.id}>
                <Link to={`/category/${item.id}`} >
                    {item.name}
                </Link>
            </Box>)}
        </div>
    )
}


export default Category
