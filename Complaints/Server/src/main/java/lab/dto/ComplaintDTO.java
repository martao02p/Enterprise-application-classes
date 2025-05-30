package lab.dto;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

import java.time.LocalDate;

public class ComplaintDTO {

    @NotNull
    private LocalDate complaintDate;

    @NotNull
    @Size(min = 1, max = 60)
    private String complaintText;

    @NotNull
    @Size(min = 1, max = 60)
    private String author;

    @NotNull
    @Size(min = 1, max = 6)
    private String status;

    public LocalDate getComplaintDate() {
        return complaintDate;
    }

    public void setComplaintDate(LocalDate complaintDate) {
        this.complaintDate = complaintDate;
    }

    public String getComplaintText() {
        return complaintText;
    }

    public void setComplaintText(String complaintText) {
        this.complaintText = complaintText;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
