package lab.app;

import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.ClientBuilder;
import jakarta.ws.rs.core.MediaType;

public class Main {
    public static void printAllComplaints(Client client) {
        var complaints = client
                .target("http://localhost:8080/Server-1.0-SNAPSHOT/api/complaints")
                .request(MediaType.APPLICATION_JSON)
                .get(String.class);

        System.out.println("All complaints:\n" + complaints);
    }

    public static String getComplaint(Client client, Long id) {
        return client
                .target("http://localhost:8080/Server-1.0-SNAPSHOT/api/complaints/" + id)
                .request(MediaType.APPLICATION_JSON)
                .get(String.class);
    }

    public static void closeComplaint(Client client, Long id, String updatedJson) {
        client
                .target("http://localhost:8080/Server-1.0-SNAPSHOT/api/complaints/" + id)
                .request()
                .put(jakarta.ws.rs.client.Entity.entity(updatedJson, MediaType.APPLICATION_JSON));
    }

    public static void getOpenComplaints(Client client) {
        var open = client
                .target("http://localhost:8080/Server-1.0-SNAPSHOT/api/complaints?status=open")
                .request(MediaType.APPLICATION_JSON)
                .get(String.class);

        System.out.println("Open complaints:\n" + open);
    }


    public static void main(String[] args) {
        Client client = ClientBuilder.newClient();

        printAllComplaints(client);

        System.out.println("GET single complaint:");
        System.out.println(getComplaint(client, 2L));

        System.out.println("Updating status to closed...");
        closeComplaint(client, 2L, """
        {
          "author": "Marvin Doherty",
          "complaintDate": "2021-04-23",
          "complaintText": "Updated complaint text",
          "status": "closed"
        }
        """);

        System.out.println("Open complaints after update:");
        getOpenComplaints(client);

        client.close();
    }
}
