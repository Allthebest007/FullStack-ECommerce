import React from "react";
import {
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  TableCaption,
  Text,
} from "@chakra-ui/react";
import { useQuery } from "react-query";
import { fetchOrders } from "../../../api";
import { Link } from "react-router-dom";

function Orders() {
  const { isLoading, isError, data, error } = useQuery(
    "admin:orders",
    fetchOrders
  );

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (isError) {
    return <div>Error {error.message}</div>;
  }

  console.log(data);
  return (
    <div>
      <Text fontSize="2xl" p={5}>
        Orders
      </Text>

      <Table variant="simple">
        <TableCaption>Order List</TableCaption>
        <Thead>
          <Tr>
            <Th>User</Th>
            <Th>Address</Th>
            <Th>Total Price</Th>
          </Tr>
        </Thead>
        <Tbody>
          {data.map((item) => (
            
              <Tr key={item.id}>
                
                <Td>{item.user.email}</Td>
                <Td>{item.addressInfo.deliveryAddress}</Td>
                <Td>{item.orderTotal}</Td>
                <Td><Link to={`/admin/orders/${item.id}`}>Details</Link></Td>
              </Tr>
            
          ))}
        </Tbody>
      </Table>
    </div>
  );
}

export default Orders;
